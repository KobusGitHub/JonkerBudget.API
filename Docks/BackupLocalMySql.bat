echo off
title Backup Azure MySql Database

set /p FilePath= "Please enter the backup file path?  "

cd\

cd C:\Program Files\MySQL\MySQL Server 5.7\bin

mysqldump.exe -u root -pWalle1 jonkerbudget > %FilePath%

echo You can find the backup at %FilePath%

pause