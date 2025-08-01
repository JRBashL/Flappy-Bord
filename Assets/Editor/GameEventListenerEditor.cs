using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameEventListener))]
public class GameEventListenerInspector: Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        // This line is supposed to be for syncing the serialized fields with the actual values. A way to keep updating the editor in real time
        // I guess
        serializedObject.Update();

        // I don't need to wrap the GameEventListener into a serialized object because it's already been wrapped
        // In this line [CustomEditor(typeof(GameEventListener))]. So the code below we don't need
        // SerializedObject objgameEventListener = serializedObject.FindProperty("GameEventListener")

        // Then I need to get the serialized properties from the object
        SerializedProperty propOnEventTriggeredOneFloat = serializedObject.FindProperty("onEventTriggeredOneFloat");
        SerializedProperty proponEventTriggeredOneInt = serializedObject.FindProperty("onEventTriggeredOneInt");
        SerializedProperty proponEventTriggeredOneBool = serializedObject.FindProperty("onEventTriggeredOneBool");
        SerializedProperty proponEventTriggeredOneString = serializedObject.FindProperty("onEventTriggeredOneString");

        SerializedProperty propOnEventTriggeredTwoFloat = serializedObject.FindProperty("onEventTriggeredTwoFloat");
        SerializedProperty propOnEventTriggeredTwoInt = serializedObject.FindProperty("onEventTriggeredTwoInt");
        SerializedProperty propOnEventTriggeredTwoBool = serializedObject.FindProperty("onEventTriggeredTwoBool");
        SerializedProperty propOnEventTriggeredTwoString = serializedObject.FindProperty("onEventTriggeredTwoString");

        SerializedProperty propOnEventTriggeredThreeFloat = serializedObject.FindProperty("onEventTriggeredThreeFloat");
        SerializedProperty propOnEventTriggeredThreeInt = serializedObject.FindProperty("onEventTriggeredThreeInt");
        SerializedProperty propOnEventTriggeredThreeBool = serializedObject.FindProperty("onEventTriggeredThreeBool");
        SerializedProperty propOnEventTriggeredThreeString = serializedObject.FindProperty("onEventTriggeredThreeString");

        SerializedProperty propOnEventTriggeredFourFloat = serializedObject.FindProperty("onEventTriggeredFourFloat");
        SerializedProperty propOnEventTriggeredFourInt = serializedObject.FindProperty("onEventTriggeredFourInt");
        SerializedProperty propOnEventTriggeredFourBool = serializedObject.FindProperty("onEventTriggeredFourBool");
        SerializedProperty propOnEventTriggeredFourString = serializedObject.FindProperty("onEventTriggeredFourString");

        // Then I need to initialize property fields with these serialized properties
        PropertyField fieldOnEventTriggeredOneFloat = new PropertyField(propOnEventTriggeredOneFloat);
        PropertyField fieldOnEventTriggeredOneInt = new PropertyField(proponEventTriggeredOneInt);
        PropertyField fieldOnEventTriggeredOneBool = new PropertyField(proponEventTriggeredOneBool);
        PropertyField fieldOnEventTriggeredOneString = new PropertyField(proponEventTriggeredOneString);

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


        return base.CreateInspectorGUI();
    }
    
}
