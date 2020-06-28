# f-sharp-minesweeper

F# の SPA でマインスイーパを作ったやつ

```bash
$ dotnet build -o dist
$ cp -r src/f-sharp-minesweeper.Client/wwwroot/* ./dist/wwwroot/
$ npx http-server dist/wwwroot/
```