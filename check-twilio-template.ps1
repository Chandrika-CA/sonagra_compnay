# Lists the WhatsApp/Content templates of the Twilio account the website uses,
# and shows the exact Content SID to paste. The Auth Token is read from user-secrets and is never printed.
$ErrorActionPreference = 'Stop'
$project = Join-Path $PSScriptRoot 'SonagraCompany'
Push-Location $project
try {
    $secrets = @{}
    foreach ($line in (dotnet user-secrets list)) {
        $i = $line.IndexOf(' = ')
        if ($i -gt 0) { $secrets[$line.Substring(0, $i)] = $line.Substring($i + 3) }
    }
    $settings = Get-Content (Join-Path $project 'appsettings.json') -Raw | ConvertFrom-Json
    $t = $settings.Contact.Twilio

    $sid   = if ($secrets['Contact:Twilio:AccountSid']) { $secrets['Contact:Twilio:AccountSid'] } else { $t.AccountSid }
    $token = if ($secrets['Contact:Twilio:AuthToken'])  { $secrets['Contact:Twilio:AuthToken'] }  else { $t.AuthToken }
    $configured = if ($secrets['Contact:Twilio:WhatsAppContentSid']) { $secrets['Contact:Twilio:WhatsAppContentSid'] } else { $t.WhatsAppContentSid }

    $cred = New-Object System.Management.Automation.PSCredential($sid, (ConvertTo-SecureString $token -AsPlainText -Force))
    Write-Host "Account: $sid"
    Write-Host "ContentSid currently used by the website: $configured"
    Write-Host ''

    $list = Invoke-RestMethod -Uri 'https://content.twilio.com/v1/Content?PageSize=50' -Credential $cred
    Write-Host "Templates found in this account: $($list.contents.Count)"
    foreach ($c in $list.contents) {
        $vars = if ($c.variables) { ($c.variables.PSObject.Properties.Name -join ', ') } else { '(none)' }
        $mark = if ($c.sid -eq $configured) { '  <== matches the website setting' } else { '' }
        Write-Host ("{0}  {1}  variables: {2}{3}" -f $c.sid, $c.friendly_name, $vars, $mark)
    }

    $match = $list.contents | Where-Object { $_.sid -eq $configured }
    if (-not $match) {
        Write-Host ''
        Write-Host 'The website ContentSid is NOT in this account. Copy the correct SID above and run:' -ForegroundColor Yellow
        Write-Host '  dotnet user-secrets set "Contact:Twilio:WhatsAppContentSid" "<SID from the list>"' -ForegroundColor Yellow
    }
    else {
        Write-Host ''
        Write-Host 'ContentSid is valid for this account.' -ForegroundColor Green
        try {
            $ap = Invoke-RestMethod -Uri "https://content.twilio.com/v1/Content/$configured/ApprovalRequests" -Credential $cred
            $status = $ap.whatsapp.status
            Write-Host "WhatsApp approval status: $status $($ap.whatsapp.rejection_reason)"
        } catch { Write-Host 'Could not read the WhatsApp approval status.' }
    }
}
finally { Pop-Location }
