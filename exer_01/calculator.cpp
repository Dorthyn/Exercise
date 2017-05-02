#include <stdio.h>
#include <string.h>  
#include <math.h>
#include <windows.h>
#include <time.h> //用到了time函数 
#include<malloc.h> 

#define MAX_NUM (10)
#define MAX_RE (15)  

void swap(int *a, int *b);
void div(char* result, int* dividend, int* divisor);
void round(char* sour, char* dest, int dig);
int serch(char* obj, char a);
/*

说明文档：
void swap(int *a, int *b);用于交换两个数的值；
void div(char* result, int* dividend, int* divisor);用于两位数除法运算，参数1为输出结果，参数2为被除数，参数3为除数；
void round(char* sour, char* dest, int dig);//四舍五入函数，参数1为需要四舍五入的数，参数2为结果，参数3为保留位数；
                                            //当要保留的位数大于实际小数位数时，返回原值；
int serch(char* obj, char a);//查找函数，返回字符a的实际位置；

主函数实现【随机出10道两位数加减乘除分数运算的计算器，并返回正确结果及做对题目的个数】的功能
Date：2017-05-02
*/
int main()
{

    int i;
    int number_1;
    int number_2;
    int ope_t;
    int val;//用户输入结果
    int rig = 0;
    int wro = 0;
    float res_0;
    char val_0[MAX_RE];//除法的输入结果

    int res;//正确结果
    char *str_1[] = { "+","-","×","÷","/" };
    srand((unsigned)time(NULL)); //用时间做种，每次产生随机数不一样
    printf("Put in answers of following questions below:\n");
    for (i = 0; i < MAX_NUM; i++)
    {
        number_1 = rand() % 100; //产生0-99的随机数
        number_2 = rand() % 100;
        ope_t = rand() % 5;//五种运算符
                           //ope_t = 4;//用来调试分式运算
        if (ope_t < 4) {
            printf("第%d题：%d%s%d = ", (i + 1), number_1, str_1[ope_t], number_2);
            if (ope_t != 3) {
                switch (ope_t) {
                case 0:
                    res = number_1 + number_2;
                    break;
                case 1:
                    res = number_1 - number_2;
                    break;
                case 2:
                    res = number_1 * number_2;
                    break;
                default:
                    printf("运行错误！");
                }

                scanf("%d", &val);

                if (val == res)
                {
                    printf("回答正确\n");
                    rig++;
                }
                else
                {
                    printf("回答错误，正确答案为%d\n", res);
                    wro++;
                }
            }
            else
            {//除法
                char* re;//正确结果
                char* re_r;//四舍五入的结果

                re = (char*)malloc(MAX_RE);
                re_r = (char*)malloc(MAX_RE);

                if (!re) {
                    printf("内存不足！");
                    return -1;
                }

                if (!re_r) {
                    printf("内存不足！");
                    return -1;
                }

                memset(re, 0, MAX_RE);
                memset(re_r, 0, MAX_RE);

                div(re, &number_1, &number_2);

                round(re, re_r, 10);
                scanf("%s", val_0);

                if (0 == strcmp(re, val_0))
                {
                    printf("回答正确\n");
                    rig++;
                }
                else
                {
                    printf("回答错误，正确答案为%s\n", re_r);
                    wro++;
                }

                free(re);
                free(re_r);
            }

        }
        else//ope_t=4，即为分数运算时
        {
            int number_3;
            int number_4;
            char res_fr[100] = "";//用来存放计算结果
            char res_fr_1[3];
            char res_fr_2[3];
            char res_inp[10];//用来存放输入结果
                             //itoa(number,string,10);

            number_3 = rand() % 10 + 1; //产生1-10的随机数
            number_4 = rand() % 10 + 1;

            int ope_t2 = rand() % 4;//分数的加减乘除运算
                                    //number_1和number_2，number_3和number_4交换，保证分母比分子大
            if (number_2 < number_1) {

                swap(&number_1, &number_2);
            }

            if (number_4 < number_3) {

                swap(&number_3, &number_4);
            }

            printf("第%d题：%d/%d %s %d/%d = ", (i + 1), number_1, number_2, str_1[ope_t2], number_3, number_4);

            switch (ope_t2) {
            case 0:
                if (number_2 == number_4) {//同分母运算
                    itoa((number_1 + number_3), res_fr_1, 10);
                    strcat(res_fr, res_fr_1);

                    strcat(res_fr, "/");

                    itoa(number_2, res_fr_2, 10);
                    strcat(res_fr, res_fr_2);

                }
                else {
                    itoa((number_1 * number_4 + number_2 * number_3), res_fr_1, 10);//不同分母运算的分子
                    strcat(res_fr, res_fr_1);

                    strcat(res_fr, "/");

                    itoa((number_2 * number_4), res_fr_2, 10);
                    strcat(res_fr, res_fr_2);
                }
                break;
            case 1://减法运算
                if (number_2 == number_4) {//同分母运算
                    itoa((number_1 - number_3), res_fr_1, 10);
                    strcat(res_fr, res_fr_1);

                    strcat(res_fr, "/");

                    itoa(number_2, res_fr_2, 10);
                    strcat(res_fr, res_fr_2);

                }
                else {
                    itoa((number_1 * number_4 - number_2 * number_3), res_fr_1, 10);//不同分母运算的分子
                    strcat(res_fr, res_fr_1);

                    strcat(res_fr, "/");

                    itoa((number_2 * number_4), res_fr_2, 10);
                    strcat(res_fr, res_fr_2);
                }
                break;
            case 2://乘法运算
                itoa((number_1 * number_3), res_fr_1, 10);
                strcat(res_fr, res_fr_1);

                strcat(res_fr, "/");

                itoa((number_2 * number_4), res_fr_2, 10);
                strcat(res_fr, res_fr_2);
                break;
            case 3://除法运算
                itoa((number_1 * number_4), res_fr_1, 10);
                strcat(res_fr, res_fr_1);

                strcat(res_fr, "/");

                itoa((number_2 * number_3), res_fr_2, 10);
                strcat(res_fr, res_fr_2);
                break;
            }
            scanf("%s", res_inp);
            //strcmp(str_1, str_2) == 0
            if (strcmp(res_fr, res_inp) == 0)
            {
                printf("回答正确\n");
                rig++;
            }
            else
            {
                printf("回答错误，正确答案为%s\n", res_fr);
                wro++;
            }
        }
    }

    printf("共答对%d道，答错%d道", rig, wro);

    system("pause");
    return 0;
}

void swap(int *a, int *b) {//交换a和b的值
    int temp;
    temp = *a;
    *a = *b;
    *b = temp;
}
void div(char *r, int *a, int* b) {//除法函数,a为被除数，b为除数
    int count = 0;
    int c2 = 0;
    int c3 = 0;//小数位数-被除数小于除数时
    int c4 = 0;//小数位数-被除数大于除数时

    if ((*a) / (*b) != 0) {
        while ((*a != 0) && c4 < 11) {

            if ((*a) >(*b)) {
                r[count++] = *a / *b + '0';
                *a = (*a) % (*b);
                if (c2 > 0)
                    ++c4;
            }
            else {
                c2++;
                if (c2 == 1)
                    r[count++] = '.';
                *a *= 10;
            }
        }//while
    }
    else {//a<b的情况
        r[count++] = '0';
        r[count++] = '.';

        *a *= 10;

        while ((*a != 0) && c3 < 11) {
            if ((*a) >(*b)) {
                r[count++] = *a / *b + '0';
                *a = (*a) % (*b);
                ++c3;
            }
            else {
                *a *= 10;
                if ((*a) < (*b)) {

                    r[count++] = '0';
                    ++c3;
                }
            }
        }//while
    }
}

void round(char* sour, char* dest, int dig) {//取余函数
    int len_1 = strlen(sour);
    int len_2 = serch(sour, '.');
    int len_3 = len_1 - len_2;//实际小数位数
    int len_4 = len_2 + dig - 1;

    if (len_3 > dig) {
        int la = sour[len_2 + dig] - '0';

        if ((la + 5) > 9) {
            for (int i = 0; i < len_4; i++) {
                dest[i] = sour[i];
            }
            dest[len_4] = sour[len_4] - '0' + 1 + '0';
        }
        else
            for (int i = 0; i < len_4; i++) {
                dest[i] = sour[i];
            }
    }
    else
        //printf("保留位数应该小于实际小数位数");
        for (int i = 0; i < len_1; i++) {
            dest[i] = sour[i];
        }
}

int serch(char* obj, char a) {//计算字符a在字符串string中的位置  
    char  c = '.';
    char* ptr = strchr(obj, c);
    int pos = ptr - obj + 1;
    if (ptr)
    {
        //printf("The character [%c] was found at pos: [%d]\n", c, pos);
        return pos;
    }
    else
    {
        //printf("The character was not found\n");
        return -1;
    }
}