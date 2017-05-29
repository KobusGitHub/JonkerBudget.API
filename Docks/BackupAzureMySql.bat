echo off
title Backup Azure MySql Database

set /p FilePath= "Please enter the backup file path?  "


cd\

cd C:\Program Files\MySQL\MySQL Server 5.7\bin

mysqldump.exe -h jonkerbudget.mysql.database.azure.com -u azureroot@jonkerbudget -p jonkerbudget > %FilePath%

echo You can find the backup at c:\temp\jonkerBudgetBackup.sql

pause