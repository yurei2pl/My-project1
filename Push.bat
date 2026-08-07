@echo off
setlocal

echo === Git: Dodawanie wybranych plików ===
echo Aktualny status repozytorium:
git status

echo.
echo Wpisz nazwy plików do dodania (oddziel spacjami), np.:
echo   git add plik1.txt plik2.cs
echo lub wciskaj Enter, aby pominąć ten krok.
set /p FILES=
if not "%FILES%"=="" (
    echo Dodawanie plików: %FILES%
    git add %FILES%
)

echo.
echo Aktualny status po dodaniu:
git status

echo.
echo Wpisz komunikat commita:
set /p COMMIT_MSG=
if not "%COMMIT_MSG%"=="" (
    git commit -m "%COMMIT_MSG%"
)

echo.
echo Czy wypchnąć zmiany na GitHub? (T/N)
set /p PUSH_CONFIRM=
if /i "%PUSH_CONFIRM%"=="T" (
    git push
) else (
    echo Pominięto push.
)

endlocal