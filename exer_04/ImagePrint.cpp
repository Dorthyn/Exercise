/*
������ʵ��ͼ�δ�ӡ
DATA��2017-05-08
TIME:13:14
AUTHOR:DORTHYN
*/
#include <stdio.h>
#include <windows.h>

#define ROW 4

int main()
{
    int i = 0;
    int j = 0;
    int k = 0;
    for(int n = 0;n < ROW;n++)
    {
      //  int count = 0;

            for (i = 0; i < 3; i++)
            {
               for (int m = 0; m < (3 * (3 - n)); m++)//����ո�
                 {
                     printf(" ");
                 }

               for(int count = 0;count < (n+1);count++)
               {
                   for (k = 0; k < (2 - i); k++)//��λ�ո�
                   {
                       printf(" ");
                   }
                   for (j = 0; j < (2 * i + 1); j++)
                   {
                       printf("*");
                   }
                   for (int p = 0; p < (3 - i); p++)//��λ��ո� 
                   {
                       printf(" ");
                   }
               }

               printf("\n");
            }
    }

    system("pause");
    return 0;
}