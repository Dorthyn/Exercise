#include <stdio.h>
#include <string.h>  
#include <math.h>
#include <windows.h>
#include <time.h> //用到了time函数 
#include<malloc.h> 

#define MAX_NUM (10)

void swap(int *, int *);
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
    float val_0;

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
                    //		case 3:
                    //		res = number_1 / number_2;
                    //	res = (int)(100.0 * res + 0.5) / 100.0;
                    break;
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
            {
                res_0 = number_1 / number_2;
                //res_0= (int)(100.0 * res + 0.5) / 100.0;
                scanf("%f", &val_0);

                if ((val_0 - res_0) < 0.001)
                {
                    printf("回答正确\n");
                    rig++;
                }
                else
                {
                    printf("回答错误，正确答案为%f\n", res_0);
                    wro++;
                }


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

            /////////////////////////////////////////////
            //res_fr = (char *)malloc(20);
            //	if (res_fr)
            //	printf("Not Enough Memory!\n");
            //exit;

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

void swap(int *a, int *b) {
    int temp;
    temp = *a;
    *a = *b;
    *b = temp;
}