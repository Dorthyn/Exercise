/*
本函数实现打印万年历：
1.只输入年份打印全年日历；
2.输入年-月打印该月日历；
3.输入年-月-日打印周几
DATA:2017-05-09
TIME:09:00
AUTHOR:DORTHYN
*/
#include <stdio.h>
#include <windows.h>

int ISLeapYear(int year);//判断是否为闰年
int Week(int year);//返回输入年份的元旦距离1999年12月31日的周几
void YearPrint(int year, int first_wk);//按年打印输出
int ISOddMonth(int mon);
void MonthPrint(int year, int mon, int first_wk);//按月打印输出,传入first_wk为元旦的周几
int Week_wk(int year, int mon, int day, int first_wk);//返回、输入日期是周几

int main() {
    int val_year = 0;
    int val_month = 0;
    int val_day = 0;

    printf("输入年-月-日,例如：2017-01-01\n");
    scanf_s("%d-%d-%d", &val_year, &val_month, &val_day);//输入年份—测试

    int wk_p = Week(val_year);

    if ((0 == val_month) && (0 == val_day))//输出全年
    {
        YearPrint(val_year, wk_p);
    }

    else if (0 == val_day)//输出月
    {
        MonthPrint(val_year, val_month, wk_p);
    }
    else//输出周几
    {
        char *wed[7] = { "日","一","二","三","四","五","六"};
        int wk_temp = 0;

        wk_temp = Week_wk(val_year, val_month, val_day, wk_p);

        printf("%d-%d-%d为周%s\n", val_year,val_month,val_day,wed[wk_temp]);
    }

    system("pause");
    return 0;
}

int ISLeapYear(int year)//判断是否为闰年
{
    int bool_leap = 0;
    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
        bool_leap = 1;//是闰年
    else
        bool_leap = 0;//不是闰年
    return  bool_leap;
}

int Week(int year)//返回输入年份的元旦距离1999年12月31日的周几，周日为0
{
    int count = 0;
    int wk = 0;//作为返回值
    for (int i = year; (i - 1999) > 0; i--)
    {
        if (ISLeapYear(i) == 1)
        {
            count += 366;
        }
        else
        {
            count += 365;
        }
    }
    wk = ((count - 1) % 7 + 6) % 7;
    return wk;
}
void YearPrint(int year, int first_wk)//按年打印输出
{
    for (int mon_count = 0; mon_count < 12; mon_count++)
    {
        MonthPrint(year, (mon_count + 1), first_wk);
    }
}

int ISOddMonth(int mon)//判断是否为大月
{
    if (mon == 1 || mon == 3 || mon == 5 || mon == 7 || mon == 8 || mon == 10 || mon == 12)
    {
        return 1;
    }
    else
    {
        return 0;
    }
}

void MonthPrint(int year, int mon, int first_wk)//按月打印输出,传入first_wk为元旦的周几
{
    int count = 0;//从元旦开始计算，到本月1号的天数
    int odd = 0;
    for (int i = 0; i < (mon - 1); i++)
    {
        odd = ISOddMonth(i + 1);
        if (1 == odd)//上月是大月
        {
            count += 31;
        }
        else//上月是小月
        {
            if ((1 == ISLeapYear(year)) && (1 == i))//是闰年2月
            {
                count += 29;
            }
            else if ((0 == ISLeapYear(year)) && (1 == i))//平年2月
            {
                count += 28;
            }
            else
                count += 30;
        }
    }
    first_wk = ((count - 1) % 7 + first_wk + 1) % 7;
    int tmp = first_wk;
    printf("          %d 月 份\n\n", mon);
    printf("日  一  二  三  四  五  六  \n");
    //print
    for (int i = 0; i < first_wk * 4; i++)
    {
        printf(" ");
    }
    odd = ISOddMonth(mon);
    if (1 == odd)//本是大月
    {
        for (int j = 0; j < 31; j++)
        {
            if (j < 9)
            {
                printf(" ");
            }
            printf("%d  ", j + 1);
            if (++tmp > 6)
            {
                printf("\n");
                tmp = 0;
            }
        }
    }
    else//本月是小月
    {        //判断是不是2月
        if ((1 == ISLeapYear(year)) && (2 == mon))//是闰年2月
        {
            for (int j = 0; j < 29; j++)
            {
                if (j < 9)
                {
                    printf(" ");
                }
                printf("%d  ", j + 1);
                if (++tmp > 6)
                {
                    printf("\n");
                    tmp = 0;
                }
            }
        }
        else if (0 == (ISLeapYear(year)) && (2 == mon))//是平年2月
        {
            for (int j = 0; j < 28; j++)
            {
                if (j < 9)
                {
                    printf(" ");
                }
                printf("%d  ", j + 1);
                if (++tmp > 6)
                {
                    printf("\n");
                    tmp = 0;
                }
            }
        }
        else
        {
            for (int j = 0; j < 30; j++)
            {
                if (j < 9)
                {
                    printf(" ");
                }
                printf("%d  ", j + 1);
                if (++tmp > 6)
                {
                    printf("\n");
                    tmp = 0;
                }
            }
        }
    }
    printf("\n");
    //return first_wk;
}

int Week_wk(int year, int mon, int day, int first_wk)
{
    int count = 0;//从元旦开始计算，到本月1号的天数
    int odd = 0;
    for (int i = 0; i < (mon - 1); i++)
    {
        odd = ISOddMonth(i + 1);
        if (1 == odd)//上月是大月
        {
            count += 31;
        }
        else//上月是小月
        {
            if ((1 == ISLeapYear(year)) && (1 == i))//是闰年2月
            {
                count += 29;
            }
            else if ((0 == ISLeapYear(year)) && (1 == i))//平年2月
            {
                count += 28;
            }
            else
                count += 30;
        }
    }
    count += day;
    first_wk = ((count - 1) % 7 + first_wk) % 7;
    return first_wk;
}
