using UnityEngine;
using UnityEditor;

public class SceneBuilder : MonoBehaviour
{
    [MenuItem("Tools/Build Robot Scene")]
    static void BuildScene()
    {
        AddTag("Trap");
        Material groundMat = MakeMat("GroundMat", new Color(0.3f, 0.3f, 0.35f));
        Material wallMat = MakeMat("WallMat", new Color(0.2f, 0.2f, 0.25f));
        Material rampMat = MakeMat("RampMat", new Color(0.4f, 0.35f, 0.3f));
        Material upperMat = MakeMat("UpperMat", new Color(0.25f, 0.3f, 0.35f));
        Material playerMat = MakeMat("PlayerMat", new Color(0.1f, 0.6f, 0.9f));
        Material trapMat = MakeMat("TrapMat", new Color(0.9f, 0.15f, 0.15f));
        Material bumperMat = MakeMat("BumperMat", new Color(1f, 0.5f, 0f));
        Material energyMat = MakeMat("EnergyMat", new Color(0.2f, 1f, 0.4f));
        Material speedMat = MakeMat("SpeedMat", new Color(1f, 0.1f, 0.1f));
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.name = "Ground_Lower";
        ground.transform.position = new Vector3(0f, -0.5f, 0f);
        ground.transform.localScale = new Vector3(30f, 1f, 30f);
        ground.isStatic = true;
        ground.GetComponent<Renderer>().material = groundMat;
        GameObject upper = GameObject.CreatePrimitive(PrimitiveType.Cube);
        upper.name = "Ground_Upper";
        upper.transform.position = new Vector3(0f, 2f, 18f);
        upper.transform.localScale = new Vector3(30f, 1f, 14f);
        upper.isStatic = true;
        upper.GetComponent<Renderer>().material = upperMat;
        GameObject ramp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ramp.name = "Ramp";
        ramp.transform.position = new Vector3(0f, 0.75f, 9.5f);
        ramp.transform.localScale = new Vector3(8f, 0.3f, 7f);
        ramp.transform.rotation = Quaternion.Euler(20f, 0f, 0f);
        ramp.isStatic = true;
        ramp.GetComponent<Renderer>().material = rampMat;
        MakeWall("Wall_North", new Vector3(0, 1.5f, 25), new Vector3(30, 4, 1), wallMat);
        MakeWall("Wall_South", new Vector3(0, 1.5f, -15.5f), new Vector3(30, 4, 1), wallMat);
        MakeWall("Wall_East", new Vector3(15, 1.5f, 5), new Vector3(1, 4, 42), wallMat);
        MakeWall("Wall_West", new Vector3(-15, 1.5f, 5), new Vector3(1, 4, 42), wallMat);
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        player.name = "Player";
        player.tag = "Player";
        player.transform.position = new Vector3(0f, 1f, -10f);
        player.GetComponent<Renderer>().material = playerMat;

        Rigidbody rb = player.AddComponent<Rigidbody>();
        rb.mass = 1f;
        rb.linearDamping = 0.5f;
        rb.angularDamping = 0.5f;

        PhysicsMaterial pm = new PhysicsMaterial("PlayerBounce");
        pm.bounciness = 0.3f;
        pm.dynamicFriction = 0.5f;
        pm.staticFriction = 0.5f;
        pm.bounceCombine = PhysicsMaterialCombine.Maximum;
        player.GetComponent<SphereCollider>().material = pm;

        player.AddComponent<PlayerController>();
        Camera.main.gameObject.AddComponent<CameraFollow>();
        Camera.main.GetComponent<CameraFollow>().target = player.transform;
        Camera.main.transform.position = new Vector3(0, 12, -17);
        GameObject spinParent = new GameObject("SpinningTrap");
        spinParent.transform.position = new Vector3(-5f, 0.6f, -3f);
        spinParent.tag = "Trap";

        GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bar.name = "SpinBar";
        bar.transform.SetParent(spinParent.transform);
        bar.transform.localPosition = Vector3.zero;
        bar.transform.localScale = new Vector3(6f, 0.5f, 0.5f);
        bar.GetComponent<Renderer>().material = trapMat;
        bar.tag = "Trap";

        GameObject pivot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pivot.name = "SpinPivot";
        pivot.transform.SetParent(spinParent.transform);
        pivot.transform.localPosition = new Vector3(0, -0.5f, 0);
        pivot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        pivot.GetComponent<Renderer>().material = trapMat;
        pivot.tag = "Trap";

        spinParent.AddComponent<SpinningTrap>();
        GameObject pt1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pt1.name = "PatrolTrap";
        pt1.tag = "Trap";
        pt1.transform.position = new Vector3(5f, 0.5f, 0f);
        pt1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        pt1.GetComponent<Renderer>().material = trapMat;
        PatrolTrap p1 = pt1.AddComponent<PatrolTrap>();
        p1.pointA = new Vector3(5, 0.5f, -5);
        p1.pointB = new Vector3(5, 0.5f, 5);
        p1.moveSpeed = 4f;

        GameObject pt2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pt2.name = "PatrolTrap_Upper";
        pt2.tag = "Trap";
        pt2.transform.position = new Vector3(-3f, 3f, 18f);
        pt2.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        pt2.GetComponent<Renderer>().material = trapMat;
        PatrolTrap p2 = pt2.AddComponent<PatrolTrap>();
        p2.pointA = new Vector3(-8, 3, 18);
        p2.pointB = new Vector3(8, 3, 18);
        p2.moveSpeed = 5f;
        GameObject b1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        b1.name = "BumperPad";
        b1.transform.position = new Vector3(8f, 0.1f, -8f);
        b1.transform.localScale = new Vector3(2f, 0.15f, 2f);
        b1.GetComponent<Renderer>().material = bumperMat;
        b1.AddComponent<BumperPad>();

        GameObject b2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        b2.name = "BumperPad_Upper";
        b2.transform.position = new Vector3(5f, 2.6f, 20f);
        b2.transform.localScale = new Vector3(2f, 0.15f, 2f);
        b2.GetComponent<Renderer>().material = bumperMat;
        b2.AddComponent<BumperPad>();
        MakeEnergy("EnergyCore_1", new Vector3(-6, 1.5f, -5), energyMat);
        MakeEnergy("EnergyCore_2", new Vector3(8, 1.5f, 3), energyMat);
        MakeEnergy("EnergyCore_3", new Vector3(0, 4, 16), energyMat);
        MakeEnergy("EnergyCore_4", new Vector3(-7, 4, 22), energyMat);
        MakeEnergy("EnergyCore_5", new Vector3(7, 4, 20), energyMat);
        MakeSpeed("SpeedBoost_1", new Vector3(0, 1.5f, -3), speedMat);
        MakeSpeed("SpeedBoost_2", new Vector3(3, 4, 22), speedMat);

        Debug.Log("=== SCENE ROBOT VUOT DIA HINH DA DUNG XONG ===");
    }

    static void MakeWall(string n, Vector3 p, Vector3 s, Material m)
    {
        GameObject w = GameObject.CreatePrimitive(PrimitiveType.Cube);
        w.name = n; w.transform.position = p; w.transform.localScale = s;
        w.isStatic = true; w.GetComponent<Renderer>().material = m;
    }

    static void MakeEnergy(string n, Vector3 p, Material m)
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.name = n; o.transform.position = p;
        o.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        o.GetComponent<Renderer>().material = m;
        o.GetComponent<BoxCollider>().isTrigger = true;
        o.AddComponent<EnergyCore>();
    }

    static void MakeSpeed(string n, Vector3 p, Material m)
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        o.name = n; o.transform.position = p;
        o.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        o.GetComponent<Renderer>().material = m;
        o.GetComponent<SphereCollider>().isTrigger = true;
        o.AddComponent<SpeedBoost>();
    }

    static Material MakeMat(string n, Color c)
    {
        Material m = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        m.name = n; m.SetColor("_BaseColor", c); return m;
    }

    static void AddTag(string tag)
    {
        var tm = new SerializedObject(AssetDatabase.LoadMainAssetAtPath("ProjectSettings/TagManager.asset"));
        var tags = tm.FindProperty("tags");
        for (int i = 0; i < tags.arraySize; i++)
            if (tags.GetArrayElementAtIndex(i).stringValue == tag) return;
        tags.InsertArrayElementAtIndex(tags.arraySize);
        tags.GetArrayElementAtIndex(tags.arraySize - 1).stringValue = tag;
        tm.ApplyModifiedProperties();
    }
}
