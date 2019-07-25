$username = "ansible"
$password = "Password@123"
$secstr = New-Object -TypeName System.Security.SecureString
$password.ToCharArray() | ForEach-Object {$secstr.AppendChar($_)}
$cred = new-object -typename System.Management.Automation.PSCredential -argumentlist $username, $secstr

New-SSHSession -ComputerName 10.1.150.2 -Credential $cred

$IP = Invoke-SSHCommand -Index 0 -Command "cat /tmp/clusterip.txt " | Format-List -Property Output |Out-File -FilePath e:\ip.txt

$input_path = "e:\ip.txt"
$output_file = 'e:\ipextracted.txt'
$regex = '\d{1,3}(\.\d{1,3}){3}'
select-string -Path $input_path -Pattern $regex -AllMatches | % { $_.Matches } | % { $_.Value } > $output_file