#include <stdio.h>
#include <string.h>  
#include <math.h>
#include <windows.h>
#include <time.h> //�õ���time���� 

#define MAX_NUM (10)


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
	float val_0;

	int res;//��ȷ���
	char *str_1[] = { "+","-","��","��" };
	srand((unsigned)time(NULL)); //��ʱ�����֣�ÿ�β����������һ��
	printf("Put in answers of following questions below:\n");
	for (i = 0; i < MAX_NUM; i++)
	{
		number_1 = rand() % 100; //����0-99�������
		number_2 = rand() % 100;
		ope_t = rand() % 4;
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
				//		case 3:
				//		res = number_1 / number_2;
				//	res = (int)(100.0 * res + 0.5) / 100.0;
				break;
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
		{
			res_0 = number_1 / number_2;
			//res_0= (int)(100.0 * res + 0.5) / 100.0;
			scanf("%f", &val_0);

			if ((val_0 - res_0)<0.001)
			{
				printf("�ش���ȷ\n");
				rig++;
			}
			else
			{
				printf("�ش������ȷ��Ϊ%f\n", res_0);
				wro++;
			}


		}
	}
	printf("�����%d�������%d��", rig, wro);
	system("pause");

	return 0;
}
