#include <stdio.h>
#include <stdlib.h>
#include <string.h>  
#include <math.h>
#include <windows.h>
#include <time.h> //用到了time函数 
#include <malloc.h> 
#define MAX_RE (15)
void div(char*,int*,int*);

int main() {
    //除法
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
void div(char *r, int *a, int* b) {//除法函数,a为被除数，b为除数
    int count = 0;
    int c2 = 0;
    int c3 = 0;//小数位数
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
    else {//a<b的情况
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