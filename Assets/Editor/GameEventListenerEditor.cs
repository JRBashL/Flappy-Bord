using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameEventListener))]
public class GameEventListenerInspector: Editor
{
    // Game Event Property
    SerializedProperty propGameEvent;

    //Group Zero - Static Call Event
    SerializedProperty propOnEventTriggered;

    // Group One - One Param Dynamic Call Event
    SerializedProperty propOnEventTriggeredOneFloat, propOnEventTriggeredOneInt, 
    propOnEventTriggeredOneBool, propOnEventTriggeredOneString;

    // Group Two - Two Param Dynamic Call Event
    SerializedProperty propOnEventTriggeredTwoFloat, propOnEventTriggeredTwoInt, 
    propOnEventTriggeredTwoBool, propOnEventTriggeredTwoString;

    // Group Three - Three Param Dynamic Call Event
    SerializedProperty propOnEventTriggeredThreeFloat, propOnEventTriggeredThreeInt, 
    propOnEventTriggeredThreeBool, propOnEventTriggeredThreeString;

    // Group Four - Four Param Dynamic Call Event
    SerializedProperty propOnEventTriggeredFourFloat, propOnEventTriggeredFourInt, 
    propOnEventTriggeredFourBool, propOnEventTriggeredFourString;

    void OnEnable()
    {
        // I need to get the serialized properties from the object
        propGameEvent = serializedObject.FindProperty("GameEvent");

        propOnEventTriggered = serializedObject.FindProperty("onEventTriggered"); 

        propOnEventTriggeredOneFloat  = serializedObject.FindProperty("onEventTriggeredOneFloat");
        propOnEventTriggeredOneInt    = serializedObject.FindProperty("onEventTriggeredOneInt");
        propOnEventTriggeredOneBool   = serializedObject.FindProperty("onEventTriggeredOneBool");
        propOnEventTriggeredOneString = serializedObject.FindProperty("onEventTriggeredOneString");

        propOnEventTriggeredTwoFloat  = serializedObject.FindProperty("onEventTriggeredTwoFloat");
        propOnEventTriggeredTwoInt    = serializedObject.FindProperty("onEventTriggeredTwoInt");
        propOnEventTriggeredTwoBool   = serializedObject.FindProperty("onEventTriggeredTwoBool");
        propOnEventTriggeredTwoString = serializedObject.FindProperty("onEventTriggeredTwoString");

        propOnEventTriggeredThreeFloat  = serializedObject.FindProperty("onEventTriggeredThreeFloat");
        propOnEventTriggeredThreeInt    = serializedObject.FindProperty("onEventTriggeredThreeInt");
        propOnEventTriggeredThreeBool   = serializedObject.FindProperty("onEventTriggeredThreeBool");
        propOnEventTriggeredThreeString = serializedObject.FindProperty("onEventTriggeredThreeString");

        propOnEventTriggeredFourFloat  = serializedObject.FindProperty("onEventTriggeredFourFloat");
        propOnEventTriggeredFourInt    = serializedObject.FindProperty("onEventTriggeredFourInt");
        propOnEventTriggeredFourBool   = serializedObject.FindProperty("onEventTriggeredFourBool");
        propOnEventTriggeredFourString = serializedObject.FindProperty("onEventTriggeredFourString");
    }
    public override VisualElement CreateInspectorGUI()
    {
        // This line is supposed to be for syncing the serialized fields with the actual values. A way to keep updating the editor in real time
        // I guess
        serializedObject.Update();

        // Create a root Visual Element as a "stage" to hold the heirarchy
        VisualElement rootInspector = new VisualElement();

        // I don't need to wrap the GameEventListener into a serialized object because it's already been wrapped
        // In this line [CustomEditor(typeof(GameEventListener))]. So the code below we don't need
        // SerializedObject objgameEventListener = serializedObject.FindProperty("GameEventListener")

        // I need to initialize property fields with these serialized properties
        PropertyField fieldGameEvent = new PropertyField(propGameEvent);

        PropertyField fieldOnEventTriggered = new PropertyField(propOnEventTriggered);

        PropertyField fieldOnEventTriggeredOneFloat = new PropertyField(propOnEventTriggeredOneFloat);
        PropertyField fieldOnEventTriggeredOneInt = new PropertyField(propOnEventTriggeredOneInt);
        PropertyField fieldOnEventTriggeredOneBool = new PropertyField(propOnEventTriggeredOneBool);
        PropertyField fieldOnEventTriggeredOneString = new PropertyField(propOnEventTriggeredOneString);

        PropertyField fieldOnEventTriggeredTwoFloat = new PropertyField(propOnEventTriggeredTwoFloat);
        PropertyField fieldOnEventTriggeredTwoInt = new PropertyField(propOnEventTriggeredTwoInt);
        PropertyField fieldOnEventTriggeredTwoBool = new PropertyField(propOnEventTriggeredTwoBool);
        PropertyField fieldOnEventTriggeredTwoString = new PropertyField(propOnEventTriggeredTwoString);

        PropertyField fieldOnEventTriggeredThreeFloat = new PropertyField(propOnEventTriggeredThreeFloat);
        PropertyField fieldOnEventTriggeredThreeInt = new PropertyField(propOnEventTriggeredThreeInt);
        PropertyField fieldOnEventTriggeredThreeBool = new PropertyField(propOnEventTriggeredThreeBool);
        PropertyField fieldOnEventTriggeredThreeString = new PropertyField(propOnEventTriggeredThreeString);

        PropertyField fieldOnEventTriggeredFourFloat = new PropertyField(propOnEventTriggeredFourFloat);
        PropertyField fieldOnEventTriggeredFourInt = new PropertyField(propOnEventTriggeredFourInt);
        PropertyField fieldOnEventTriggeredFourBool = new PropertyField(propOnEventTriggeredFourBool);
        PropertyField fieldOnEventTriggeredFourString = new PropertyField(propOnEventTriggeredFourString);

        // Create foldouts and make them collaped by default
        Foldout oneParamFO = new Foldout();
        oneParamFO.text = "1 Param Events";
        oneParamFO.value = false;

        Foldout twoParamFO = new Foldout();
        twoParamFO.text = "2 Param Events";
        twoParamFO.value = false;

        Foldout threeParamFO = new Foldout();
        threeParamFO.text = "3 Param Events";
        threeParamFO.value = false;

        Foldout fourParamFO = new Foldout();
        fourParamFO.text = "4 Param Events";
        fourParamFO.value = false;

        // Add stuff to foldouts
        oneParamFO.Add(fieldOnEventTriggeredOneFloat);
        oneParamFO.Add(fieldOnEventTriggeredOneInt);
        oneParamFO.Add(fieldOnEventTriggeredOneBool);
        oneParamFO.Add(fieldOnEventTriggeredOneString);

        twoParamFO.Add(fieldOnEventTriggeredTwoFloat);
        twoParamFO.Add(fieldOnEventTriggeredTwoInt);
        twoParamFO.Add(fieldOnEventTriggeredTwoBool);
        twoParamFO.Add(fieldOnEventTriggeredTwoString);

        threeParamFO.Add(fieldOnEventTriggeredThreeFloat);
        threeParamFO.Add(fieldOnEventTriggeredThreeInt);
        threeParamFO.Add(fieldOnEventTriggeredThreeBool);
        threeParamFO.Add(fieldOnEventTriggeredThreeString);

        fourParamFO.Add(fieldOnEventTriggeredFourFloat);
        fourParamFO.Add(fieldOnEventTriggeredFourInt);
        fourParamFO.Add(fieldOnEventTriggeredFourBool);
        fourParamFO.Add(fieldOnEventTriggeredFourString);

        //Add to the UI heirarchy
        rootInspector.Add(fieldGameEvent);

        rootInspector.Add(fieldOnEventTriggered);

        rootInspector.Add(oneParamFO);
        rootInspector.Add(twoParamFO);
        rootInspector.Add(threeParamFO);
        rootInspector.Add(fourParamFO);

        return rootInspector;
    }
    
}
