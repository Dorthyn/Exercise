#include <stdio.h>
#include <stdlib.h>
#include <string.h>  
#include <math.h>
#include <windows.h>
#include <time.h> //�õ���time���� 
#include <malloc.h> 
#define MAX_RE (15)
void div(char*,int*,int*);

int main() {
    //����
    int number_1;
    int number_2;

    for (int i = 0; i < 10; i++) {
        scanf_s("%d%d", &number_1, &number_2);
        char* re;
        re = (char*)malloc(MAX_RE);
        memset(re, 0, MAX_RE);
        div(re, &number_1, &number_2);
        printf("%s", re);
        free(re);
    }
    system("pause");
    return 0;
}
void div(char *r, int *a, int* b) {//��������,aΪ��������bΪ����
    int count = 0;
    int c2 = 0;
    int c3 = 0;//С��λ��
    if ((*a) / (*b) != 0) {
        while ((*a != 0) && count < 13) {

            if (*a > *b) {
                r[count++] = *a / *b + '0';
                *a = (*a) % (*b);

            }
            else {

                c2++;
                if (c2 == 1)
                    r[count++] = '.';
                *a *= 10;
            }
            ++c3;
        }//while
    }
    else {//a<b�����
        r[count++] = '0';
        r[count++] = '.';
        *a *= 10;
        while ((*a != 0) && c3 < 10) {
            if ((*a) > (*b)) {
                r[count++] = *a / *b + '0';
                *a = (*a) % (*b);
            }
            else {
                    *a *= 10;
                    if ((*a) < (*b)) {
                    
                        r[count++] = '0';
                    }
            }

            ++c3;
        }//while

    }


}