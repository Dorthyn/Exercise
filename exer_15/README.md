# C# 实现读取进程信息

## V0.1

**本程序实现控制台下读取进程名称信息(非管理员模式)**

> DATA:2017-09-06
>
> TIME:11:08
>
> AUTHOR:DORTHYN

利用**NtQuerySystemInformation **获取进程信息，仿windows下的任务管理器相关实现，win32平台下的控制台应用程序。

**本次练习的难点**：全部的任务是做任务管理器的进程界面和性能界面，但是未知事件太多，很多原本以为不需要怎么查询就能找得到的进程信息查询&调试了好久，说明提前规划解决问题的重要性。再者，`NtQuerySystemInformation `是系统库函数，也是第一次仿写实现系统库函数的功能，应用attribute特性。仿写程序在附件中。
