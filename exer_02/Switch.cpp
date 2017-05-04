/*
主函数实现千亿以内人民币的大小写转换，精确到分
File:Te.cpp
Author:Ma Qiang
Date:2017-05-04
Time:10:25
*/

// 头文件
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <windows.h>
#include <malloc.h> 

// 宏定义
#define MAX_CAPACITY 50
#define MBILL 2
#define TENTHOUSAND 1
#define YUAN 0
#define MAX_PARA 5


// 函数声明
void partition(char *Lower_case, char *Upper_case);//将数字分成三小段；
void read(char *temp, char *result);//将段内千位以内数字大写读出来；
void reverse(char s[]);//转置函数；
int ISdecimals(char *temp); //判断是否为小数，是则返回小数点所在位置，不是则返回0；

// 代码实现
int main() {
    //整数金额
    char Lower_amount[MAX_CAPACITY] = { 0 };
    char *Upper_amount;
    char *Upp_m[10] = { "零","壹","贰","叁","肆","伍","陆","柒","捌","玖" };

    int Lower_amount_lenth = 0;

    Upper_amount = (char*)malloc(MAX_CAPACITY);

    if (!Upper_amount)
    {
        printf("内存不足！");
        return -1;
    }
    memset(Upper_amount, 0, MAX_CAPACITY);

    printf_s("输入要转换的小写金额：\n");
    scanf_s("%s", Lower_amount,15);
    //
    int Judge = ISdecimals(Lower_amount);
    if (0 == Judge)//是整数
    {
        partition(Lower_amount, Upper_amount);
        strcat_s(Upper_amount, MAX_CAPACITY, "圆");
        strcat_s(Upper_amount, MAX_CAPACITY, "整");
    }
    else//不是整数
    {
        Lower_amount_lenth = strlen(Lower_amount);
        char Lower_amount_temp1[MAX_CAPACITY] = { 0 };//用来存整数部分

        for (int i = 0; i < (Judge - 1); i++)
        {
            Lower_amount_temp1[i] = Lower_amount[i];
        }

        partition(Lower_amount_temp1, Upper_amount);//XXX圆
        strcat_s(Upper_amount, sizeof(Upper_amount), "圆");

        char Lower_amount_temp2[MAX_CAPACITY] = { 0 };//用来存小数部分
        int JudgeCopy = Judge;

        for (int i = 0; i < (Lower_amount_lenth - Judge + 1); i++)
        {
            Lower_amount_temp2[i] = Lower_amount[JudgeCopy++];
        }

        int temp_integer = Lower_amount_temp2[0] - '0';

        strcat_s(Upper_amount, sizeof(Upper_amount), Upp_m[temp_integer]);
        strcat_s(Upper_amount, sizeof(Upper_amount), "角");

        if (2 == Lower_amount_lenth - Judge)

        {
            temp_integer = Lower_amount_temp2[1] - '0';
            strcat_s(Upper_amount, MAX_CAPACITY, Upp_m[temp_integer]);
            strcat_s(Upper_amount, MAX_CAPACITY, "分");
        }
    }


    printf("转换大写金额为\n%s\n", Upper_amount);

    free(Upper_amount);

    system("pause");
    return 0;
}

void partition(char *Lower_case, char *Upper_case)
{
    char tmp[3][5] = { 0 };//多申请一位\0，因为后面要作为字符串处理

    int Lower_case_len = strlen(Lower_case);
    int i = 0;
    int k = 0;
    int m = 0;

    for (int j = 0; j < Lower_case_len; j++)
    {
        if (0 == (j % 4))
        {
            ++k;
            m = k - 1;
        }
        tmp[m][j % 4] = Lower_case[Lower_case_len - j - 1];


    }

    if (m >= 1)
    {
        for (int j = 0; j < 4; j++)
        {
            char temp = tmp[m][j];
            tmp[m][j] = tmp[0][j];
            tmp[0][j] = temp;
        }
    }

    for (int i = 0; i <= m; i++)
    {
        reverse(tmp[i]);
    }
    //根据tmp中的值确定

    int mm = m;
    int tmp_lenth = 0;

    //为read申请的临时区，以便判断返回输出是否加单位
    char *Read_amount;
    Read_amount = (char*)malloc(MAX_PARA);

    if (!Read_amount)
    {
        printf("内存不足！");
    }

    for (int j = 0; j <= m; j++)
    {
        memset(Read_amount, 0, MAX_PARA);
        read(tmp[j], Read_amount);

        int rel = strcmp(Read_amount, "零");

        if (MBILL == mm)
        {
            strcat_s(Upper_case, MAX_CAPACITY, Read_amount);
            strcat_s(Upper_case, MAX_CAPACITY, "亿");
        }
        if ((TENTHOUSAND == mm) && (rel != 0))
        {
            strcat_s(Upper_case, MAX_CAPACITY, Read_amount);
            strcat_s(Upper_case, MAX_CAPACITY, "万");
        }
        if (YUAN == mm)
        {
            if (rel != 0)
            {
                strcat_s(Upper_case, MAX_CAPACITY, Read_amount);
            }
            /*           else
            {
            strcat(Upper_case, "圆");
            }*/
        }
        --mm;
    }
    free(Read_amount);
}

void read(char *temp, char *result)//一维数组传参 
{
    char *Upp_1[3] = { "千","百","拾" };
    char *Upp_2[10] = { "零","壹","贰","叁","肆","伍","陆","柒","捌","玖" };
    int len_temp = strlen(temp);
    int buf_0 = temp[0] - '0';
    int buf_1 = 0;
    int buf_2 = 0;
    int buf_3 = 0;

    switch (len_temp)
    {
    case 1:
        strcat_s(result, MAX_PARA, Upp_2[buf_0]);//存个位数的大写
        break;
    case 2:
        strcat_s(result, MAX_PARA, Upp_2[buf_0]);//存十位数的大写
        strcat_s(result, MAX_PARA, Upp_1[2]);//拾
                                 //char *tmp_1[1] = { 0 };
        if ('0' != temp[1])
        {
            buf_1 = temp[1] - '0';
            strcat_s(result, sizeof(result), Upp_2[buf_1]);//存个位数的大写
        }
        break;
    case 3:
        strcat_s(result, MAX_PARA, Upp_2[buf_0]);//存百位数的大写
        strcat_s(result, MAX_PARA, Upp_1[1]);//百

        if ('0' != temp[1])//十位数不为0
        {
            buf_1 = temp[1] - '0';
            strcat_s(result, MAX_PARA, Upp_2[buf_1]);//存十位数的大写
            strcat_s(result, MAX_PARA, Upp_1[2]);//拾
        }
        else//十位数为0
        {
            if ('0' != temp[2])
            {
                strcat_s(result, MAX_PARA, Upp_2[0]);//零
            }
        }

        buf_2 = temp[2] - '0';
        strcat_s(result, MAX_PARA, Upp_2[buf_2]);//存个位数的大写

        break;
    case 4:
        if ('0' != temp[0])//千位不为0
        {
            strcat_s(result, MAX_PARA, Upp_2[buf_0]);//存百位数的大写
            strcat_s(result, MAX_PARA, Upp_1[0]);//千
        }
        else
        {
            strcat_s(result, MAX_PARA, Upp_2[0]);//加零
        }

        if ('0' != temp[1])//百位数不为0
        {
            buf_1 = temp[1] - '0';
            strcat_s(result, MAX_PARA, Upp_2[buf_1]);//存百位数的大写
            strcat_s(result, MAX_PARA, Upp_1[1]);//百
            if ('0' != temp[2])//十位数不为0
            {
                buf_2 = temp[2] - '0';
                strcat_s(result, MAX_PARA, Upp_2[buf_2]);//存十位数的大写
                strcat_s(result, MAX_PARA, Upp_1[2]);//拾
            }
            else//十位数为0
            {
                if ('0' != temp[3])
                {
                    strcat_s(result, MAX_PARA, Upp_2[0]);//零
                }
            }
            buf_3 = temp[3] - '0';
            strcat_s(result, MAX_PARA, Upp_2[buf_3]);//存个位数的大写
        }
        else//百位数为0
        {
            if (('0' != temp[2]) || ('0' != temp[3])) //十位数或个位数至少有一个不为0
            {
                strcat_s(result, MAX_PARA, Upp_2[0]);//零
                if ('0' == temp[2])//十位数为0，个位数不为0
                {
                    buf_1 = temp[3] - '0';
                    strcat_s(result, MAX_PARA, Upp_2[buf_1]);//存个位数的大写
                }
                else if ('0' == temp[3])//个位数为0，十位数不为0
                {
                    buf_2 = temp[2] - '0';
                    strcat_s(result, MAX_PARA, Upp_2[buf_2]);//存十位数的大写
                    strcat_s(result, MAX_PARA, Upp_1[2]);//拾
                }
                else//都不为0
                {
                    buf_2 = temp[2] - '0';
                    strcat_s(result, MAX_PARA, Upp_2[buf_2]);//存十位数的大写
                    strcat_s(result, MAX_PARA, Upp_1[2]);//拾
                    buf_3 = temp[3] - '0';
                    strcat_s(result, MAX_PARA, Upp_2[buf_3]);//存个位数的大写
                }

            }
        }
        break;
    default:
        printf("出错！");
        break;
    }
}
void reverse(char s[])
{
    int i = 0, j;
    char c;
    j = strlen(s) - 1;
    while (i<j)
    {
        c = s[i]; s[i++] = s[j]; s[j--] = c;
    }
}

int ISdecimals(char *temp) //判断是否为小数，是则返回小数点所在位置，不是则返回0
{
    char  c = '.';
    char* ptr = strchr(temp, c);
    int pos = ptr - temp + 1;
    if (ptr)
    {
        //printf("The character [%c] was found at pos: [%d]\n", c, pos);
        return pos;
    }
    else
    {
        //printf("The character was not found\n");
        return 0;
    }
}