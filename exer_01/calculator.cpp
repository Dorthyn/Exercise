#include <stdio.h>
#include <string.h>  
#include <math.h>
#include <windows.h>
#include <time.h> //�õ���time���� 
#include<malloc.h> 

#define MAX_NUM (10)
#define MAX_RE (15)  

void swap(int *a, int *b);
void div(char* result, int* dividend, int* divisor);
void round(char* sour, char* dest, int dig);
int serch(char* obj, char a);
/*

˵���ĵ���
void swap(int *a, int *b);���ڽ�����������ֵ��
void div(char* result, int* dividend, int* divisor);������λ���������㣬����1Ϊ������������2Ϊ������������3Ϊ������
void round(char* sour, char* dest, int dig);//�������뺯��������1Ϊ��Ҫ�����������������2Ϊ���������3Ϊ����λ����
                                            //��Ҫ������λ������ʵ��С��λ��ʱ������ԭֵ��
int serch(char* obj, char a);//���Һ����������ַ�a��ʵ��λ�ã�

������ʵ�֡������10����λ���Ӽ��˳���������ļ���������������ȷ�����������Ŀ�ĸ������Ĺ���
Date��2017-05-02
*/
int main()
{

    int i;
    int number_1;
    int number_2;
    int ope_t;
    int val;//�û�������
    int rig = 0;
    int wro = 0;
    float res_0;
    char val_0[MAX_RE];//������������

    int res;//��ȷ���
    char *str_1[] = { "+","-","��","��","/" };
    srand((unsigned)time(NULL)); //��ʱ�����֣�ÿ�β����������һ��
    printf("Put in answers of following questions below:\n");
    for (i = 0; i < MAX_NUM; i++)
    {
        number_1 = rand() % 100; //����0-99�������
        number_2 = rand() % 100;
        ope_t = rand() % 5;//���������
                           //ope_t = 4;//�������Է�ʽ����
        if (ope_t < 4) {
            printf("��%d�⣺%d%s%d = ", (i + 1), number_1, str_1[ope_t], number_2);
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
                    printf("���д���");
                }

                scanf("%d", &val);

                if (val == res)
                {
                    printf("�ش���ȷ\n");
                    rig++;
                }
                else
                {
                    printf("�ش������ȷ��Ϊ%d\n", res);
                    wro++;
                }
            }
            else
            {//����
                char* re;//��ȷ���
                char* re_r;//��������Ľ��

                re = (char*)malloc(MAX_RE);
                re_r = (char*)malloc(MAX_RE);

                if (!re) {
                    printf("�ڴ治�㣡");
                    return -1;
                }

                if (!re_r) {
                    printf("�ڴ治�㣡");
                    return -1;
                }

                memset(re, 0, MAX_RE);
                memset(re_r, 0, MAX_RE);

                div(re, &number_1, &number_2);

                round(re, re_r, 10);
                scanf("%s", val_0);

                if (0 == strcmp(re, val_0))
                {
                    printf("�ش���ȷ\n");
                    rig++;
                }
                else
                {
                    printf("�ش������ȷ��Ϊ%s\n", re_r);
                    wro++;
                }

                free(re);
                free(re_r);
            }

        }
        else//ope_t=4����Ϊ��������ʱ
        {
            int number_3;
            int number_4;
            char res_fr[100] = "";//������ż�����
            char res_fr_1[3];
            char res_fr_2[3];
            char res_inp[10];//�������������
                             //itoa(number,string,10);

            number_3 = rand() % 10 + 1; //����1-10�������
            number_4 = rand() % 10 + 1;

            int ope_t2 = rand() % 4;//�����ļӼ��˳�����
                                    //number_1��number_2��number_3��number_4��������֤��ĸ�ȷ��Ӵ�
            if (number_2 < number_1) {

                swap(&number_1, &number_2);
            }

            if (number_4 < number_3) {

                swap(&number_3, &number_4);
            }

            printf("��%d�⣺%d/%d %s %d/%d = ", (i + 1), number_1, number_2, str_1[ope_t2], number_3, number_4);

            switch (ope_t2) {
            case 0:
                if (number_2 == number_4) {//ͬ��ĸ����
                    itoa((number_1 + number_3), res_fr_1, 10);
                    strcat(res_fr, res_fr_1);

                    strcat(res_fr, "/");

                    itoa(number_2, res_fr_2, 10);
                    strcat(res_fr, res_fr_2);

                }
                else {
                    itoa((number_1 * number_4 + number_2 * number_3), res_fr_1, 10);//��ͬ��ĸ����ķ���
                    strcat(res_fr, res_fr_1);

                    strcat(res_fr, "/");

                    itoa((number_2 * number_4), res_fr_2, 10);
                    strcat(res_fr, res_fr_2);
                }
                break;
            case 1://��������
                if (number_2 == number_4) {//ͬ��ĸ����
                    itoa((number_1 - number_3), res_fr_1, 10);
                    strcat(res_fr, res_fr_1);

                    strcat(res_fr, "/");

                    itoa(number_2, res_fr_2, 10);
                    strcat(res_fr, res_fr_2);

                }
                else {
                    itoa((number_1 * number_4 - number_2 * number_3), res_fr_1, 10);//��ͬ��ĸ����ķ���
                    strcat(res_fr, res_fr_1);

                    strcat(res_fr, "/");

                    itoa((number_2 * number_4), res_fr_2, 10);
                    strcat(res_fr, res_fr_2);
                }
                break;
            case 2://�˷�����
                itoa((number_1 * number_3), res_fr_1, 10);
                strcat(res_fr, res_fr_1);

                strcat(res_fr, "/");

                itoa((number_2 * number_4), res_fr_2, 10);
                strcat(res_fr, res_fr_2);
                break;
            case 3://��������
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
                printf("�ش���ȷ\n");
                rig++;
            }
            else
            {
                printf("�ش������ȷ��Ϊ%s\n", res_fr);
                wro++;
            }
        }
    }

    printf("�����%d�������%d��", rig, wro);

    system("pause");
    return 0;
}

void swap(int *a, int *b) {//����a��b��ֵ
    int temp;
    temp = *a;
    *a = *b;
    *b = temp;
}
void div(char *r, int *a, int* b) {//��������,aΪ��������bΪ����
    int count = 0;
    int c2 = 0;
    int c3 = 0;//С��λ��-������С�ڳ���ʱ
    int c4 = 0;//С��λ��-���������ڳ���ʱ

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
    else {//a<b�����
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

void round(char* sour, char* dest, int dig) {//ȡ�ຯ��
    int len_1 = strlen(sour);
    int len_2 = serch(sour, '.');
    int len_3 = len_1 - len_2;//ʵ��С��λ��
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
        //printf("����λ��Ӧ��С��ʵ��С��λ��");
        for (int i = 0; i < len_1; i++) {
            dest[i] = sour[i];
        }
}

int serch(char* obj, char a) {//�����ַ�a���ַ���string�е�λ��  
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