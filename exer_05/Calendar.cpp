/*
������ʵ�ִ�ӡ��������
1.ֻ������ݴ�ӡȫ��������
2.������-�´�ӡ����������
3.������-��-�մ�ӡ�ܼ�
DATA:2017-05-09
TIME:09:00
AUTHOR:DORTHYN
*/
#include <stdio.h>
#include <windows.h>

int ISLeapYear(int year);//�ж��Ƿ�Ϊ����
int Week(int year);//����������ݵ�Ԫ������1999��12��31�յ��ܼ�
void YearPrint(int year, int first_wk);//�����ӡ���
int ISOddMonth(int mon);
void MonthPrint(int year, int mon, int first_wk);//���´�ӡ���,����first_wkΪԪ�����ܼ�
int Week_wk(int year, int mon, int day, int first_wk);//���ء������������ܼ�

int main() {
    int val_year = 0;
    int val_month = 0;
    int val_day = 0;

    printf("������-��-��,���磺2017-01-01\n");
    scanf_s("%d-%d-%d", &val_year, &val_month, &val_day);//������ݡ�����

    int wk_p = Week(val_year);

    if ((0 == val_month) && (0 == val_day))//���ȫ��
    {
        YearPrint(val_year, wk_p);
    }

    else if (0 == val_day)//�����
    {
        MonthPrint(val_year, val_month, wk_p);
    }
    else//����ܼ�
    {
        char *wed[7] = { "��","һ","��","��","��","��","��"};
        int wk_temp = 0;

        wk_temp = Week_wk(val_year, val_month, val_day, wk_p);

        printf("%d-%d-%dΪ��%s\n", val_year,val_month,val_day,wed[wk_temp]);
    }

    system("pause");
    return 0;
}

int ISLeapYear(int year)//�ж��Ƿ�Ϊ����
{
    int bool_leap = 0;
    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
        bool_leap = 1;//������
    else
        bool_leap = 0;//��������
    return  bool_leap;
}

int Week(int year)//����������ݵ�Ԫ������1999��12��31�յ��ܼ�������Ϊ0
{
    int count = 0;
    int wk = 0;//��Ϊ����ֵ
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
void YearPrint(int year, int first_wk)//�����ӡ���
{
    for (int mon_count = 0; mon_count < 12; mon_count++)
    {
        MonthPrint(year, (mon_count + 1), first_wk);
    }
}

int ISOddMonth(int mon)//�ж��Ƿ�Ϊ����
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

void MonthPrint(int year, int mon, int first_wk)//���´�ӡ���,����first_wkΪԪ�����ܼ�
{
    int count = 0;//��Ԫ����ʼ���㣬������1�ŵ�����
    int odd = 0;
    for (int i = 0; i < (mon - 1); i++)
    {
        odd = ISOddMonth(i + 1);
        if (1 == odd)//�����Ǵ���
        {
            count += 31;
        }
        else//������С��
        {
            if ((1 == ISLeapYear(year)) && (1 == i))//������2��
            {
                count += 29;
            }
            else if ((0 == ISLeapYear(year)) && (1 == i))//ƽ��2��
            {
                count += 28;
            }
            else
                count += 30;
        }
    }
    first_wk = ((count - 1) % 7 + first_wk + 1) % 7;
    int tmp = first_wk;
    printf("          %d �� ��\n\n", mon);
    printf("��  һ  ��  ��  ��  ��  ��  \n");
    //print
    for (int i = 0; i < first_wk * 4; i++)
    {
        printf(" ");
    }
    odd = ISOddMonth(mon);
    if (1 == odd)//���Ǵ���
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
    else//������С��
    {        //�ж��ǲ���2��
        if ((1 == ISLeapYear(year)) && (2 == mon))//������2��
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
        else if (0 == (ISLeapYear(year)) && (2 == mon))//��ƽ��2��
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
    int count = 0;//��Ԫ����ʼ���㣬������1�ŵ�����
    int odd = 0;
    for (int i = 0; i < (mon - 1); i++)
    {
        odd = ISOddMonth(i + 1);
        if (1 == odd)//�����Ǵ���
        {
            count += 31;
        }
        else//������С��
        {
            if ((1 == ISLeapYear(year)) && (1 == i))//������2��
            {
                count += 29;
            }
            else if ((0 == ISLeapYear(year)) && (1 == i))//ƽ��2��
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
