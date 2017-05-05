/*
本程序实现20位数以内正整数的大数乘法
Date:2017-05-05
Time:14:45
Author:Dorthyn
*/
#include <stdio.h>
#include <windows.h>


int ISLegal(char *input);//输入合法性判断
int StrToIntConvert(char *str, int *integer);//字符数组转整型数组
int IntergerToSingle(int src,int *des);//将两位数字打断为单个数字并存在数组中
void Long_Number_Multiplication(int *num_1, int len_1, int *num_2, int len_2, int *result);//完成两个大数的核心运算过程

int main()
{
    const int  MAX_LENTH=20;
    char multiplier_str[MAX_LENTH] = { 0 };//乘数
    char multiplied_str[MAX_LENTH] = { 0 };//乘数


    printf("请输入乘数与被乘数：\n");
    scanf_s("%s", multiplier_str, MAX_LENTH);

    int Legal = ISLegal(multiplier_str);
    if (0 == Legal)
    {
        printf("输入不合法！");
        system("pause");
        return -1;
    }
    scanf_s("%s", multiplied_str, MAX_LENTH);
    Legal = ISLegal(multiplier_str);
    if (0 == Legal)
    {
        printf("输入不合法！");
        system("pause");
        return -1;
    }

    //分配内存（整型数组）
    int *multiplier;
    int *multiplied;
    int *product;

    int len_multiplier = strlen(multiplier_str);//乘数位数
    int len_multiplied = strlen(multiplied_str);//被乘数位数
    int len_product = len_multiplier + len_multiplied;



    multiplier = (int *)malloc(len_multiplier*sizeof(int));
    multiplied = (int *)malloc(len_multiplied*sizeof(int));
    product = (int *)malloc((len_multiplied * sizeof(int))+ (len_multiplier * sizeof(int)));
    //product[((len_multiplied * sizeof(int)) + (len_multiplier * sizeof(int)))] = {0};
    memset(product, 0, ((len_multiplied * sizeof(int)) + (len_multiplier * sizeof(int))));


    if (!multiplier)
    {
        printf("内存不足！");
        system("pause");
        return -1;
    }
    if (!multiplied)
    {
        printf("内存不足！");
        system("pause");
        return -1;
    }
    if (!product)
    {
        printf("内存不足！");
        system("pause");
        return -1;
    }
    //memset(multiplier, 0, len_multiplier);
    //memset(multiplied, 0, len_multiplied);

    //int multiplier[MAX_LENTH] = { 0 };//乘数
    //int multiplied[MAX_LENTH] = { 0 };//被乘数
    StrToIntConvert(multiplier_str, multiplier);
    //printf("multiplier[2] = %d", multiplier[2]);
    StrToIntConvert(multiplied_str, multiplied);
    //ConvertStrToInt判断返回值

    Long_Number_Multiplication(multiplier, len_multiplier, multiplied, len_multiplied, product);

    int len_result = 0;
    printf("结果为：\n");
    if (0 == product[(len_product - 1)])
        len_result = (len_product - 1) - 1;

    for (int i = len_result; i >= 0; i--)
    {
        printf("%d",product[i]);
    }


    system("pause");
    return 0;
}


int ISLegal(char *input)//输入合法性判断
{
    int input_lenth = strlen(input);
    //if (input_lenth > 12)
    //{
    //    printf("请输入千亿以内的金额!");
    //    return -1;
    //}
    for (int i = 0; i < input_lenth; i++)
    {
        if (('0' <= input[i]) && ((input[i]) <= '9'))
        {
            continue;//输入合法
        }
        else
        {
            printf("请输入正整数数字！");
            return 0;
        }
    }
    return 1;
}

int StrToIntConvert(char *str,int *integer)//字符数组转整型数组
{
    int len_str = strlen(str);
    int temp = 0;
    for (int i = 0; i < len_str; i++)
    {
        temp = str[len_str - i - 1] - '0';
        integer[i] = temp;
        //printf("integer[%d] = %d", i,integer[i]);
    }
    return len_str;
}

int IntergerToSingle(int src, int *des)//将两位数字打断为单个数字并存在数组中,当输入数字为1位数时，输出[0][x]
{
    if ((src > 9) && (src < 100))
    {
        des[0] = src % 10;
        des[1] = src / 10;
        return 1;
    }
    else
    {
        des[0] = src;
        des[1] = 0;
        return 1;
    }
}

void Long_Number_Multiplication(int *num_1,int len_1,int *num_2,int len_2,int *result)//完成两个大数的核心运算过程
                                                                  //num_1为乘数1，num_2为乘数2，
                                                                  //len_1为乘数1的长度，len_2为乘数2的长度
                                                                  //result存放乘积
{
    int k=0;
    int m = 0;
    int r_k_1 = 0;
    int r_k_2 = 0;
    int tmp[2] = { 0 };//用来存放临时个位数乘积数据
    for (int i = 0; i < len_2;i++)
    {
        m = 0;
        for (int j = i; j < (len_1  + i); j++)
        {
            int temp = num_2[i] * num_1[m++];
            IntergerToSingle(temp,tmp);
            k = j;

            r_k_1 = result[k] + tmp[0];
            result[k] = r_k_1 % 10;

            if (r_k_1 > 9) //低位需要进位
            {
                ++k;
                result[k]= result[k]+1;
                --k;
            }

            ++k;
            r_k_2 = result[k] + tmp[1];
            result[k] = r_k_2 % 10;
            if (r_k_2 > 9)//高位需要进位
            {
                ++k;
                result[k] = result[k] + 1;
                --k;
            }
        }
    }
}