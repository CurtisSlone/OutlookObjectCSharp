# Load the Outlook COM object
$outlookApp = New-Object -ComObject Outlook.Application

# Get the type of the COM object
$type = $outlookApp.GetType()

# Get the type library information
$guid = $type.GUID
$versionMajor = $type.Assembly.GetName().Version.Major
$versionMinor = $type.Assembly.GetName().Version.Minor

# Output the GUID, VersionMajor, and VersionMinor
"Outlook COM GUID: $guid"
"VersionMajor: $versionMajor"
"VersionMinor: $versionMinor"

# Release the COM object to avoid leaving the Outlook process running
[System.Runtime.InteropServices.Marshal]::ReleaseComObject($outlookApp) | Out-Null
