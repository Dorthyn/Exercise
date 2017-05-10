/*
本函数实现学生信息管理系统，未实现错误输入控制，可完成新数据的增删改查并存至本地文档；
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

Linklist *create();//创建带有头节点的链表
void print_txt(Linklist *Stu);//保存到文件
Linklist *head_update(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex, int grade);//更新节点
Linklist *head_select(Linklist *head, unsigned int ID);//查找节点
Linklist *head_delete(Linklist *head, unsigned int ID);//删除节点
Linklist *head_insert(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex, int grade);
//头插法——增加节点
int length(Linklist *head);//返回单链表长度（不含头结点）
void print_list(Linklist *head);//遍历打印
Linklist *sort(Linklist *head);//单链表的排序
Linklist *read();//读取已有数据

int main()
{
    Linklist *head;
    Linklist *result;//查找结果返回的前指针
    head = create();//创建链表
    unsigned int ID = 0;
    unsigned int Class_ID = 0;
    char Name[NAME_LG];
    int grade = 0;
    char sex[5];
    int IS_opt = 0;
    head = read();
    do {
        printf("选择要进行的操作：\n");
        printf("1.增加学生信息\n2.删除错误学生信息\n3.更新学生信息\n4.查找学生信息\n5.输出所有学生信息\n");
        int input = 0;
        scanf_s("%d", &input);
        /*Insert_stu();*/
        switch (input)
        {
        case 1:
            printf("请依次输入增加学生的学号：\n");
            scanf_s("%u", &ID);
            printf("请依次输入增加学生的班级号：\n");
            scanf_s("%u", &Class_ID);
            printf("请输入学生的姓名：\n");
            scanf_s("%s", Name, NAME_LG);
            printf("请输入学生的性别：\n");
            scanf_s("%s", sex, 5);
            printf("请输入学生的成绩：\n");
            scanf_s("%d", &grade);

            head_insert(head, ID, Class_ID, Name, sex, grade);
            break;
        case 2:
            printf("请输入要删除学生学号：\n");
            scanf_s("%u", &ID);

            head_delete(head, ID);
            break;
        case 3:
            result = head_select(head, ID);
            if (result)
            {
                printf("请依次输入更新学生的学号：\n");
                scanf_s("%u", &ID);
                printf("请依次输入更新学生的班级号：\n");
                scanf_s("%u", &Class_ID);
                printf("请输入学生的姓名：\n");
                scanf_s("%s", Name, NAME_LG);
                printf("请输入学生的性别：\n");
                scanf_s("%s", sex, 5);
                printf("请输入学生的成绩：\n");
                scanf_s("%d", &grade);

                head_update(head, ID, Class_ID, Name, sex, grade);
            }
            else
            {
                printf("查找失败！\n更新失败！\n");
            }
            break;
        case 4:
            printf("请输入要查找学生的学号：\n");
            scanf_s("%u", &ID);

            result = head_select(head, ID);

            if (result)
            {
                result = result->next;
                printf("所查找学生姓名：%s，学号：%u，班级：%u，性别：%s成绩：%d\n", result->name, result->ID, result->Class_ID, result->sex, result->grade);
            }
            else
            {
                printf("查找失败！\n");
            }
            break;
        case 5:
            print_list(head);
            break;
        default:
            printf("操作失败！\n");
        }

        printf("是否继续操作？\n1 -继续\n2 -退出\n");
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
// Qualifier: //保存txt
// Parameter: Linklist * head
//************************************
void print_txt(Linklist *head)//保存txt
{//unsigned int ID, unsigned int Clss_ID, char *Name
    Linklist *h = head;
    Linklist *p;

    FILE *fp;
    errno_t err;
    err = fopen_s(&fp, "F:\\information.txt", "w+");

    if (err == 0)
    {
        printf("文件打开成功！\n");
    }
    else
    {
        printf("文件打开失败！\n");
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
        printf("学生信息已保存至F盘！\n");
    }
    else
    {
        printf("无可存储学生信息！\n");
    }
    fclose(fp);
}

Linklist *create()//创建链表
{
    Linklist *head;
    head = (Linklist*)malloc(sizeof(Linklist));
    head->next = NULL;
    return head;
}

Linklist *head_insert(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex, int grade)
//头插法——增加节点
{
    Linklist *p;
    Linklist *h;
    h = head;
    p = (Linklist *)malloc(sizeof(Linklist));//新生节点

    p->ID = ID;
    p->Class_ID = Class_ID;
    strcpy_s(p->name, NAME_LG, Name);
    strcpy_s(p->sex, 5, sex);
    p->grade = grade;

    p->next = h->next;
    h->next = p;

    printf("添加成功！\n");
    return head;

}

Linklist *head_delete(Linklist *head, unsigned int ID)//删除节点
{
    Linklist *tmp;
    Linklist *tmp_del;
    tmp = head_select(head, ID);
    if (tmp)//找到要删除的节点的上一个节点
    {
        tmp_del = tmp->next;
        tmp->next = tmp_del->next;
        free(tmp_del);
        printf("删除成功！\n");
        return head;//成功删除，返回头结点
    }
    else
    {
        printf("删除失败，查无此人！\n");
        return NULL;
    }
}

Linklist *head_select(Linklist *head, unsigned int ID)//查找节点
{
    Linklist *p;
    Linklist *r;
    r = head;
    p = r->next;

    while ((p) && ((p->ID) != ID))
    {
        r = p;//保存上一个指针
        p = p->next;
    }
    if (!p)//没找到
    {
        printf("查无此人！\n");
        return NULL;
    }

    else //(ID == (p->ID))//找到并返回上一个指针
    {
        printf("查找成功！\n");
        return r;
    }
}

Linklist *head_update(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex, int grade)//更新节点
{
    Linklist *tmp;
    Linklist *tmp_del;
    tmp = head_select(head, ID);
    //if (tmp)//找到要改变的节点的上一个节点
    //{
    tmp_del = tmp->next;

    tmp_del->Class_ID = Class_ID;
    strcpy_s(tmp_del->name, NAME_LG, Name);
    strcpy_s(tmp_del->sex, 5, sex);
    tmp_del->grade = grade;

    printf("更新成功！\n");
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

void print_list(Linklist *head)//遍历打印
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
            printf("姓名：%s，学号：%u，班级：%u，性别：%s，成绩：%d\n", p->name, p->ID, p->Class_ID, p->sex, p->grade);
            p = p->next;
        }
        printf("\n");
    }
    else
    {
        printf("无可显示数据");
    }
}

int length(Linklist *head)//返回单链表长度（不含头结点）
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

Linklist *read()//读取信息并存储至链表
{

    unsigned int ID;
    unsigned int Class_ID;
    char name[NAME_LG];
    char sex[5];
    int grade;
    Linklist *head;
    FILE *fp;

    errno_t err;
    err = fopen_s(&fp, "F:\\information.txt", "r");//文件路径可通过字符串传递，放在形参里面

    if (err == 0)
    {
        printf("文件打开成功！\n");
    }
    else
    {
        printf("文件打开失败！\n");
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
        printf("文件关闭失败！");
    }
    else
    {
        printf("文件关闭成功！");
    }
    return head;
}
//"%s\t%u\t%u\t%s\t%d\n", p->name, p->ID, p->Class_ID, p->sex, p->grade