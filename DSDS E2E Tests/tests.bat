call npm update
call npm run webdriver-update
start cmd /k npm run webdriver-start
call npm run build
timeout 2
call npm test