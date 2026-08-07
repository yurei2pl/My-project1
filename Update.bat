@echo off
setlocal

echo === Git: Aktualizacja repozytorium z GitHuba ===

echo Pobieranie najnowszych zmian...
git fetch

echo Scalanie zmian z remote...
git pull

echo.
echo Aktualny status:
git status

endlocal