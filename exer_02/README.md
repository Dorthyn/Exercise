# 人民币大小写转换

Date:2017-05-04

Time:10:25

Author:Dorthyn

## V0.1

> 主函数实现千亿以内人民币的大小写转换，精确到分
> void partition(char *Lower_case, char *Upper_case);//将数字分成三小段；
> void read(char *temp, char *result);//将段内千位以内数字大写读出来；
> void reverse(char s[]);//转置函数；
> int ISdecimals(char *temp); //判断是否为小数，是则返回小数点所在位置，不是则返回0；

## V0.2

消除警告

2017-05-04

## V0.3

加入输入保护以及小数位四舍五入，精确到分

## V0.4

修复输入出现三位以上小数时程序输出错误的问题