# C# winform实现动态加载Form控件

## V0.1

**本程序实现利用xml生成winform界面的功能**

> DATA:2017-08-09
>
> TIME:16：10
>
> AUTHOR:DORTHYN

未实现更换语言的功能，但是有了解决思路：将load进来的文件在点击语言切换之后重新将其中对应语言的text字段取一次值。这种做法的优点就是绕过了repaint当前窗体的操作，但是这个问题是确实存在的，比如，我要一种全新的界面，不只是语言更换，这个时候就必须加载xml了。这个问题有待进一步研究和讨论。