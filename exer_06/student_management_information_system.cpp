/*
������ʵ��ѧ����Ϣ����ϵͳ��δʵ�ִ���������ƣ�����������ݵ���ɾ�Ĳ鲢���������ĵ���
DATA:2017-05-09
TIME:17:22
AUTHOR:DORTHYN
*/
#include <stdio.h>
#include <windows.h>

#define NAME_LG 10

typedef struct student
{
    unsigned int ID;
    unsigned int Class_ID;
    char name[NAME_LG];
    char sex[5];
    int grade;
    struct student *next;
}Linklist;

Linklist *create();//��������ͷ�ڵ������
void print_txt(Linklist *Stu);//���浽�ļ�
Linklist *head_update(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex, int grade);//���½ڵ�
Linklist *head_select(Linklist *head, unsigned int ID);//���ҽڵ�
Linklist *head_delete(Linklist *head, unsigned int ID);//ɾ���ڵ�
Linklist *head_insert(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex, int grade);
//ͷ�巨�������ӽڵ�
int length(Linklist *head);//���ص������ȣ�����ͷ��㣩
void print_list(Linklist *head);//������ӡ
Linklist *sort(Linklist *head);//�����������
Linklist *read();//��ȡ��������

int main()
{
    Linklist *head;
    Linklist *result;//���ҽ�����ص�ǰָ��
    head = create();//��������
    unsigned int ID = 0;
    unsigned int Class_ID = 0;
    char Name[NAME_LG];
    int grade = 0;
    char sex[5];
    int IS_opt = 0;
    head = read();
    do {
        printf("ѡ��Ҫ���еĲ�����\n");
        printf("1.����ѧ����Ϣ\n2.ɾ������ѧ����Ϣ\n3.����ѧ����Ϣ\n4.����ѧ����Ϣ\n5.�������ѧ����Ϣ\n");
        int input = 0;
        scanf_s("%d", &input);
        /*Insert_stu();*/
        switch (input)
        {
        case 1:
            printf("��������������ѧ����ѧ�ţ�\n");
            scanf_s("%u", &ID);
            printf("��������������ѧ���İ༶�ţ�\n");
            scanf_s("%u", &Class_ID);
            printf("������ѧ����������\n");
            scanf_s("%s", Name, NAME_LG);
            printf("������ѧ�����Ա�\n");
            scanf_s("%s", sex, 5);
            printf("������ѧ���ĳɼ���\n");
            scanf_s("%d", &grade);

            head_insert(head, ID, Class_ID, Name, sex, grade);
            break;
        case 2:
            printf("������Ҫɾ��ѧ��ѧ�ţ�\n");
            scanf_s("%u", &ID);

            head_delete(head, ID);
            break;
        case 3:
            result = head_select(head, ID);
            if (result)
            {
                printf("�������������ѧ����ѧ�ţ�\n");
                scanf_s("%u", &ID);
                printf("�������������ѧ���İ༶�ţ�\n");
                scanf_s("%u", &Class_ID);
                printf("������ѧ����������\n");
                scanf_s("%s", Name, NAME_LG);
                printf("������ѧ�����Ա�\n");
                scanf_s("%s", sex, 5);
                printf("������ѧ���ĳɼ���\n");
                scanf_s("%d", &grade);

                head_update(head, ID, Class_ID, Name, sex, grade);
            }
            else
            {
                printf("����ʧ�ܣ�\n����ʧ�ܣ�\n");
            }
            break;
        case 4:
            printf("������Ҫ����ѧ����ѧ�ţ�\n");
            scanf_s("%u", &ID);

            result = head_select(head, ID);

            if (result)
            {
                result = result->next;
                printf("������ѧ��������%s��ѧ�ţ�%u���༶��%u���Ա�%s�ɼ���%d\n", result->name, result->ID, result->Class_ID, result->sex, result->grade);
            }
            else
            {
                printf("����ʧ�ܣ�\n");
            }
            break;
        case 5:
            print_list(head);
            break;
        default:
            printf("����ʧ�ܣ�\n");
        }

        printf("�Ƿ����������\n1 -����\n2 -�˳�\n");
        scanf_s("%d", &IS_opt);
    } while (1 == IS_opt);

    print_txt(head);

    system("pause");
    return 0;
}

//************************************
// Method:    print_txt
// FullName:  print_txt
// Access:    public 
// Returns:   void
// Qualifier: //����txt
// Parameter: Linklist * head
//************************************
void print_txt(Linklist *head)//����txt
{//unsigned int ID, unsigned int Clss_ID, char *Name
    Linklist *h = head;
    Linklist *p;

    FILE *fp;
    errno_t err;
    err = fopen_s(&fp, "F:\\information.txt", "w+");

    if (err == 0)
    {
        printf("�ļ��򿪳ɹ���\n");
    }
    else
    {
        printf("�ļ���ʧ�ܣ�\n");
        system("pause");
        exit(-1);
    }
    p = h->next;
    if (p)
    {
        while (p)
        {
            fprintf_s(fp, "%s\t%u\t%u\t%s\t%d\n", p->name, p->ID, p->Class_ID, p->sex, p->grade);
            p = p->next;
        }
        printf("ѧ����Ϣ�ѱ�����F�̣�\n");
    }
    else
    {
        printf("�޿ɴ洢ѧ����Ϣ��\n");
    }
    fclose(fp);
}

Linklist *create()//��������
{
    Linklist *head;
    head = (Linklist*)malloc(sizeof(Linklist));
    head->next = NULL;
    return head;
}

Linklist *head_insert(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex, int grade)
//ͷ�巨�������ӽڵ�
{
    Linklist *p;
    Linklist *h;
    h = head;
    p = (Linklist *)malloc(sizeof(Linklist));//�����ڵ�

    p->ID = ID;
    p->Class_ID = Class_ID;
    strcpy_s(p->name, NAME_LG, Name);
    strcpy_s(p->sex, 5, sex);
    p->grade = grade;

    p->next = h->next;
    h->next = p;

    printf("��ӳɹ���\n");
    return head;

}

Linklist *head_delete(Linklist *head, unsigned int ID)//ɾ���ڵ�
{
    Linklist *tmp;
    Linklist *tmp_del;
    tmp = head_select(head, ID);
    if (tmp)//�ҵ�Ҫɾ���Ľڵ����һ���ڵ�
    {
        tmp_del = tmp->next;
        tmp->next = tmp_del->next;
        free(tmp_del);
        printf("ɾ���ɹ���\n");
        return head;//�ɹ�ɾ��������ͷ���
    }
    else
    {
        printf("ɾ��ʧ�ܣ����޴��ˣ�\n");
        return NULL;
    }
}

Linklist *head_select(Linklist *head, unsigned int ID)//���ҽڵ�
{
    Linklist *p;
    Linklist *r;
    r = head;
    p = r->next;

    while ((p) && ((p->ID) != ID))
    {
        r = p;//������һ��ָ��
        p = p->next;
    }
    if (!p)//û�ҵ�
    {
        printf("���޴��ˣ�\n");
        return NULL;
    }

    else //(ID == (p->ID))//�ҵ���������һ��ָ��
    {
        printf("���ҳɹ���\n");
        return r;
    }
}

Linklist *head_update(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex, int grade)//���½ڵ�
{
    Linklist *tmp;
    Linklist *tmp_del;
    tmp = head_select(head, ID);
    //if (tmp)//�ҵ�Ҫ�ı�Ľڵ����һ���ڵ�
    //{
    tmp_del = tmp->next;

    tmp_del->Class_ID = Class_ID;
    strcpy_s(tmp_del->name, NAME_LG, Name);
    strcpy_s(tmp_del->sex, 5, sex);
    tmp_del->grade = grade;

    printf("���³ɹ���\n");
    return head;
}

Linklist *sort(Linklist *head)
{
    Linklist *p;
    Linklist *h;
    int len_list = 0;
    int temp = 0;
    len_list = length(head);
    h = head;

    if ((!h) || (!(h->next)) || (!(h->next->next)))
    {
        return head;
    }

    for (int i = 1; i < len_list; i++)
    {
        p = h->next;
        for (int j = 0; j < len_list - i; j++)
        {
            if (p->grade > p->next->grade)
            {
                temp = p->grade;
                p->grade = p->next->grade;
                p->next->grade = temp;
            }
            p = p->next;
        }
    }
    return head;
}

void print_list(Linklist *head)//������ӡ
{
    Linklist *h;
    Linklist *p;
    h = sort(head);

    int len_list = 0;

    len_list = length(h);

    p = h->next;

    if (p)
    {
        while (p)
        {
            printf("������%s��ѧ�ţ�%u���༶��%u���Ա�%s���ɼ���%d\n", p->name, p->ID, p->Class_ID, p->sex, p->grade);
            p = p->next;
        }
        printf("\n");
    }
    else
    {
        printf("�޿���ʾ����");
    }
}

int length(Linklist *head)//���ص������ȣ�����ͷ��㣩
{
    int len = 0;

    Linklist *p;
    p = head->next;

    while (p != NULL)
    {
        p = p->next;
        len++;
    }
    return len;
}

Linklist *read()//��ȡ��Ϣ���洢������
{

    unsigned int ID;
    unsigned int Class_ID;
    char name[NAME_LG];
    char sex[5];
    int grade;
    Linklist *head;
    FILE *fp;

    errno_t err;
    err = fopen_s(&fp, "F:\\information.txt", "r");//�ļ�·����ͨ���ַ������ݣ������β�����

    if (err == 0)
    {
        printf("�ļ��򿪳ɹ���\n");
    }
    else
    {
        printf("�ļ���ʧ�ܣ�\n");
        system("pause");
        exit(-1);
    }

    head = create();
    while (fscanf(fp, "%s\t%u\t%u\t%s\t%d\n", name, &ID, &Class_ID, sex, &grade) != EOF) 
    {
        head_insert(head, ID, Class_ID, name, sex, grade);
        //printf("%s\t%u\t%u\t%s\n", name, id, cid, sex);
    }

    if (fclose(fp) != 0)
    {
        printf("�ļ��ر�ʧ�ܣ�");
    }
    else
    {
        printf("�ļ��رճɹ���");
    }
    return head;
}
//"%s\t%u\t%u\t%s\t%d\n", p->name, p->ID, p->Class_ID, p->sex, p->grade