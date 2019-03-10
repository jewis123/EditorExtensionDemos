using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MenuItems : MonoBehaviour {
    public int experience = 0;


    //菜单栏MenuItem
    [MenuItem ("Tools/MenuItemTest/SimpleItem")] //指定MenuItem路径
    public static void SimpleLog () {
        Debug.Log ("Log From MenuItem"); //简单的打印一条日志
    }

    //SimpleItem的验证函数
    //设置SimpleItem可点击条件：函数返回true才可执行SimpleLog函数
    [MenuItem ("Tools/MenuItemTest/SimpleItem", true, 100)] //0:优先级：越大越靠后
    public static bool LogWithValidation () {
        return Application.isPlaying; //运行状态才能点击
    }
    //END


    //组件区域MenuItem
    //这个类可以传递上下文信息，可以用来修改其他组件数据
    [MenuItem ("CONTEXT/Rigidbody/MassSet")]
    static void MassSet (MenuCommand command) {
        //这一句就是具体含义
        Rigidbody body = (Rigidbody) command.context;
        body.mass = 5;
        Debug.Log ("Changed Rigidbody's Mass to " + body.mass + " from Context Menu...");
    }



    //在属性条目上右击调出menu item
    [ContextMenuItem ("Randomize Name", "Randomize")] 
    public string Scenename;
    private void Randomize () {
        Scenename = "Some Random Name";
    }


    //自定义组件上下文菜单，函数是非静态的,用来设置组件变量是极好的
    [ContextMenu ("ContextMenu Func")]
    void DoSomething () {
        experience = 10000;
    }
    //END
}