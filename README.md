# AccountOwnerServer
CodeMaze .Net Core Series Example [Link](https://code-maze.com/net-core-series/)

## My Notes

`launchSettings.json` file which contains some configurations about project and you can modify them.
### NLog
* Create interface for `ILoggingManager` service in `Contacts`.
* Create `LoggerManager` class that is implemented `LoggerManager` in `LoggerService`.
* Install NLog.Extension.Logging in `LoggerService`.
* Create `nlog.config` file in main project directory. And set configurations in file.
* Register LoggerManager service into ConfigureServices method in Startup.cs
