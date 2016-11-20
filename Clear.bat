@echo clear...
for /r "src" %%f in (.) Do If %%~nf == bin rd /s/q "%%f"
for /r "src" %%f in (.) Do If %%~nf == obj rd /s/q "%%f"

@echo rd /s/q "src/packages"
rd /s/q "src/TestResults"
@pause