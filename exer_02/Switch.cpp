/*
������ʵ��ǧ����������ҵĴ�Сдת������ȷ����
File:Te.cpp
Author:Ma Qiang
Date:2017-05-04
Time:10:25
*/

// ͷ�ļ�
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <windows.h>
#include <malloc.h> 

// �궨��
#define MAX_CAPACITY 50
#define MBILL 2
#define TENTHOUSAND 1
#define YUAN 0
#define MAX_PARA 20


// ��������
void partition(char *Lower_case, char *Upper_case);//�����ֳַ���С�Σ�
void read(char *temp, char *result);//������ǧλ�������ִ�д��������
void reverse(char s[]);//ת�ú�����
int ISdecimals(char *temp); //�ж��Ƿ�ΪС�������򷵻�С��������λ�ã������򷵻�0��
int ISLegal(char *input);//�ж�����Ϸ���
void round(char *source, int dig);//�������뺯��������1Ϊ��Ҫ�����������������2Ϊ���������3Ϊ����λ����
                                            //��Ҫ������λ������ʵ��С��λ��ʱ������ԭֵ��
int search(char* obj);//�����ַ�'.'���ַ���string�е�λ��

                     // ����ʵ��

int main() {
    //�������
    char Lower_amount[MAX_CAPACITY] = { 0 };
    char *Upper_amount;
    char *Upp_m[10] = { "��","Ҽ","��","��","��","��","½","��","��","��" };

    int Lower_amount_lenth = 0;

    Upper_amount = (char*)malloc(MAX_CAPACITY);

    if (!Upper_amount)
    {
        printf("�ڴ治�㣡");
        return -1;
    }
    memset(Upper_amount, 0, MAX_CAPACITY);

    printf_s("����Ҫת����Сд��\n");
    scanf_s("%s", Lower_amount, MAX_CAPACITY);
    //

    if ((-1) == ISLegal(Lower_amount))
    {
        printf("���벻�Ϸ��������˳���");
        system("pause");
        return -1;
    }


    int Judge = ISdecimals(Lower_amount);
    if (0 == Judge)//������
    {
        if (strlen(Lower_amount) > 12)
        {
            printf("������ǧ�����ڵĽ��!");
            return -1;
        }
        partition(Lower_amount, Upper_amount);
        strcat_s(Upper_amount, MAX_CAPACITY, "Բ");
        strcat_s(Upper_amount, MAX_CAPACITY, "��");
    }
    else//��������
    {
        if ('0'==Lower_amount[0])
        {
            strcat_s(Upper_amount, MAX_CAPACITY, "��");
        }
        char *Lower_amount_temp;
        Lower_amount_temp = (char*)malloc(MAX_CAPACITY);
        if (!Lower_amount_temp)
        {
            printf("�ڴ治�㣡");
            return -1;
        }
        memset(Lower_amount_temp, 0, MAX_CAPACITY);

        char Lower_amount_temp1[MAX_CAPACITY] = { 0 };//��������������


        for (int i = 0; i < (Judge - 1); i++)
        {
            Lower_amount_temp1[i] = Lower_amount[i];
        }

        if (strlen(Lower_amount_temp1) > 12)
        {
            printf("������ǧ�����ڵĽ��!");
            return -1;
        }

        partition(Lower_amount_temp1, Upper_amount);//XXXԲ
        strcat_s(Upper_amount, MAX_CAPACITY, "Բ");

        round(Lower_amount, 2);
        Lower_amount_lenth = strlen(Lower_amount);
        char Lower_amount_temp2[MAX_CAPACITY] = { 0 };//������С������
        int JudgeCopy = Judge;

        for (int i = 0; i < (Lower_amount_lenth - Judge + 1); i++)
        {
            Lower_amount_temp2[i] = Lower_amount[JudgeCopy++];
        }
        //printf("round�صĽ����%s",Lower_amount);
        int temp_integer = Lower_amount_temp2[0] - '0';

        strcat_s(Upper_amount, MAX_CAPACITY, Upp_m[temp_integer]);
        strcat_s(Upper_amount, MAX_CAPACITY, "��");

        if (2 == Lower_amount_lenth - Judge)

        {
            temp_integer = Lower_amount_temp2[1] - '0';
            strcat_s(Upper_amount, MAX_CAPACITY, Upp_m[temp_integer]);
            strcat_s(Upper_amount, MAX_CAPACITY, "��");
        }
    }


    printf("ת����д���Ϊ\n%s\n", Upper_amount);

    free(Upper_amount);

    system("pause");
    return 0;
}

void partition(char *Lower_case, char *Upper_case)
{
    char tmp[3][5] = { 0 };//������һλ\0����Ϊ����Ҫ��Ϊ�ַ�������

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
    //����tmp�е�ֵȷ��

    int mm = m;
    int tmp_lenth = 0;

    //Ϊread�������ʱ�����Ա��жϷ�������Ƿ�ӵ�λ
    char *Read_amount;
    Read_amount = (char*)malloc(MAX_PARA);

    if (!Read_amount)
    {
        printf("�ڴ治�㣡");
    }

    for (int j = 0; j <= m; j++)
    {
        memset(Read_amount, 0, MAX_PARA);
        read(tmp[j], Read_amount);

        int rel = strcmp(Read_amount, "��");

        if (MBILL == mm)
        {
            strcat_s(Upper_case, MAX_CAPACITY, Read_amount);
            strcat_s(Upper_case, MAX_CAPACITY, "��");
        }
        if ((TENTHOUSAND == mm) && (rel != 0))
        {
            strcat_s(Upper_case, MAX_CAPACITY, Read_amount);
            strcat_s(Upper_case, MAX_CAPACITY, "��");
        }
        if (YUAN == mm)
        {
            if (rel != 0)
            {
                strcat_s(Upper_case, MAX_CAPACITY, Read_amount);
            }
            /*           else
            {
            strcat(Upper_case, "Բ");
            }*/
        }
        --mm;
    }
    free(Read_amount);
}

void read(char *temp, char *result)//һά���鴫�� 
{
    char *Upp_1[3] = { "ǧ","��","ʰ" };
    char *Upp_2[10] = { "��","Ҽ","��","��","��","��","½","��","��","��" };
    int len_temp = strlen(temp);
    int buf_0 = temp[0] - '0';
    int buf_1 = 0;
    int buf_2 = 0;
    int buf_3 = 0;

    switch (len_temp)
    {
    case 1:
        strcat_s(result, MAX_PARA, Upp_2[buf_0]);//���λ���Ĵ�д
        break;
    case 2:
        strcat_s(result, MAX_PARA, Upp_2[buf_0]);//��ʮλ���Ĵ�д
        strcat_s(result, MAX_PARA, Upp_1[2]);//ʰ
                                             //char *tmp_1[1] = { 0 };
        if ('0' != temp[1])
        {
            buf_1 = temp[1] - '0';
            strcat_s(result, MAX_PARA, Upp_2[buf_1]);//���λ���Ĵ�д
        }
        break;
    case 3:
        strcat_s(result, MAX_PARA, Upp_2[buf_0]);//���λ���Ĵ�д
        strcat_s(result, MAX_PARA, Upp_1[1]);//��

        if ('0' != temp[1])//ʮλ����Ϊ0
        {
            buf_1 = temp[1] - '0';
            strcat_s(result, MAX_PARA, Upp_2[buf_1]);//��ʮλ���Ĵ�д
            strcat_s(result, MAX_PARA, Upp_1[2]);//ʰ
        }
        else//ʮλ��Ϊ0
        {
            if ('0' != temp[2])
            {
                strcat_s(result, MAX_PARA, Upp_2[0]);//��
            }
        }

        buf_2 = temp[2] - '0';
        strcat_s(result, MAX_PARA, Upp_2[buf_2]);//���λ���Ĵ�д

        break;
    case 4:
        if ('0' != temp[0])//ǧλ��Ϊ0
        {
            strcat_s(result, MAX_PARA, Upp_2[buf_0]);//���λ���Ĵ�д
            strcat_s(result, MAX_PARA, Upp_1[0]);//ǧ
        }
        else
        {
            strcat_s(result, MAX_PARA, Upp_2[0]);//����
        }

        if ('0' != temp[1])//��λ����Ϊ0
        {
            buf_1 = temp[1] - '0';
            strcat_s(result, MAX_PARA, Upp_2[buf_1]);//���λ���Ĵ�д
            strcat_s(result, MAX_PARA, Upp_1[1]);//��
            if ('0' != temp[2])//ʮλ����Ϊ0
            {
                buf_2 = temp[2] - '0';
                strcat_s(result, MAX_PARA, Upp_2[buf_2]);//��ʮλ���Ĵ�д
                strcat_s(result, MAX_PARA, Upp_1[2]);//ʰ
            }
            else//ʮλ��Ϊ0
            {
                if ('0' != temp[3])
                {
                    strcat_s(result, MAX_PARA, Upp_2[0]);//��
                }
            }
            buf_3 = temp[3] - '0';
            strcat_s(result, MAX_PARA, Upp_2[buf_3]);//���λ���Ĵ�д
        }
        else//��λ��Ϊ0
        {
            if (('0' != temp[2]) || ('0' != temp[3])) //ʮλ�����λ��������һ����Ϊ0
            {
                strcat_s(result, MAX_PARA, Upp_2[0]);//��
                if ('0' == temp[2])//ʮλ��Ϊ0����λ����Ϊ0
                {
                    buf_1 = temp[3] - '0';
                    strcat_s(result, MAX_PARA, Upp_2[buf_1]);//���λ���Ĵ�д
                }
                else if ('0' == temp[3])//��λ��Ϊ0��ʮλ����Ϊ0
                {
                    buf_2 = temp[2] - '0';
                    strcat_s(result, MAX_PARA, Upp_2[buf_2]);//��ʮλ���Ĵ�д
                    strcat_s(result, MAX_PARA, Upp_1[2]);//ʰ
                }
                else//����Ϊ0
                {
                    buf_2 = temp[2] - '0';
                    strcat_s(result, MAX_PARA, Upp_2[buf_2]);//��ʮλ���Ĵ�д
                    strcat_s(result, MAX_PARA, Upp_1[2]);//ʰ
                    buf_3 = temp[3] - '0';
                    strcat_s(result, MAX_PARA, Upp_2[buf_3]);//���λ���Ĵ�д
                }

            }
        }
        break;
    default:
        printf("����");
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

int ISdecimals(char *temp) //�ж��Ƿ�ΪС�������򷵻�С��������λ�ã������򷵻�0
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
void round(char *source, int dig)
{
    int pos_dot = 0;
    int len_source = strlen(source);//�ַ�����
    pos_dot = search(source);

    int result_dig = pos_dot + dig;//Ҫ�����λ��

    char *temp;
    temp = (char *)malloc(MAX_CAPACITY);

    if (!temp)
    {
        printf("�ڴ治�㣡");
    }

    memset(temp, 0, MAX_CAPACITY);

    memcpy(temp, source, result_dig);

    if ((len_source - dig - pos_dot) > 0)
    {
        int t1 = source[dig + pos_dot] - '0';
        if ((t1 + 5) > 9)
        {
            int carry_bit = source[dig + pos_dot - 1] - '0';
            ++carry_bit;
            temp[dig + pos_dot - 1] = carry_bit + '0';
        }
    }

    memcpy(source, temp, MAX_CAPACITY);

}

int search(char* obj) {//�����ַ�'.'���ַ���string�е�λ��  
    char  c = '.';
    char* ptr = strchr(obj, c);
    if (ptr)
    {
        int pos = ptr - obj + 1;
        //printf("The character [%c] was found at pos: [%d]\n", c, pos);
        return pos;
    }
    else
    {
        //printf("The character was not found\n");
        return -1;
    }
}

int ISLegal(char *input)
{
    int input_lenth = strlen(input);
    //if (input_lenth > 12)
    //{
    //    printf("������ǧ�����ڵĽ��!");
    //    return -1;
    //}
    int DotAmount = 0;
    for (int i = 0; i < input_lenth; i++)
    {
        if ((('0' <= input[i]) && (input[i]) <= '9') || (input[i] == '.'))
        {
            if (input[i] == '.')
            {
                ++DotAmount;
                if (DotAmount > 1)
                {
                    printf("�������ֻ������һ��С����!\n");
                    return -1;
                }
            }
        }
        else
        {
            printf("���������֣�");
            return -1;
        }
    }
    return 1;
}