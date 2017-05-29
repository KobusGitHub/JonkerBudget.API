echo off
title Restore to Local MySql Database

set /p FilePath= "Please enter the backup file path?  "


cd\

cd C:\Program Files\MySQL\MySQL Server 5.7\bin

mysql.exe -uroot -pWalle1 jonkerbudget < %FilePath%

echo Restore Done!!!

pause