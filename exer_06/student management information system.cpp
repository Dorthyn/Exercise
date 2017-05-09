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
    struct student *next;
}Linklist;


//void Insert_stu();
Linklist *create();//创建带有头节点的链表
void print_stu(Linklist *Stu);//保存到文件
Linklist *head_update(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex);//更新节点
Linklist *head_select(Linklist *head, unsigned int ID);//查找节点
Linklist *head_delete(Linklist *head, unsigned int ID);//删除节点
Linklist *head_insert(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex);
                                                      //头插法——增加节点

int main() 
{
    Linklist *head;
    Linklist *result;//查找结果返回的前指针
    head = create();//创建链表
    unsigned int ID = 0;
    unsigned int Class_ID = 0;
    char Name[NAME_LG];
    char sex[5];
    int IS_opt = 0;
    do {
        printf("选择要进行的操作：\n");
        printf("1.增加学生信息\n2.删除错误学生信息\n3.更新学生信息\n4.查找学生信息\n");
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
            head_insert(head, ID, Class_ID, Name, sex);
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
                head_update(head, ID, Class_ID, Name, sex);
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
                printf("所查找学生姓名：%s，学号：%u，班级：%u，性别：%s\n", result->name, result->ID, result->Class_ID, result->sex);
            }
            else
            {
                printf("查找失败！\n");
            }
            break;
        default:
            printf("操作失败！\n");
        }

        printf("是否继续操作？\n1 -继续\n2 -退出\n");
        scanf_s("%d", &IS_opt);
    } while (1 == IS_opt);

    print_stu(head);

    system("pause");
    return 0;
}

void print_stu(Linklist *head)//保存txt
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
            fprintf_s(fp, "姓名：%s\t学号：%u\t班级：%u\t性别：%s\n", p->name, p->ID, p->Class_ID, p->sex);
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

Linklist *create()
{
    Linklist *head;
    head = (Linklist*)malloc(sizeof(Linklist));
    head->next = NULL;
    return head;
}

Linklist *head_insert(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex)
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

    p->next = h->next;
    h->next = p;
    
    printf("添加成功！\n");
    return head;

}

Linklist *head_delete(Linklist *head,unsigned int ID)//删除节点
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

Linklist *head_update(Linklist *head, unsigned int ID, unsigned int Class_ID, char *Name, char *sex)//更新节点
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
        printf("更新成功！\n");
        return head;
    //}
    //else
    //{
    //    printf("查无此人！\n");
    //    return NULL;
    //}
}