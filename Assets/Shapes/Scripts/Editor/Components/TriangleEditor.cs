using UnityEditor;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes {

	[CustomEditor( typeof(Triangle) )]
	[CanEditMultipleObjects]
	public class TriangleEditor : ShapeRendererEditor {

		SerializedProperty propA = null;
		SerializedProperty propB = null;
		SerializedProperty propC = null;
		SerializedProperty propColorMode = null;
		SerializedProperty propColorB = null;
		SerializedProperty propColorC = null;

		public override void OnInspectorGUI() {
			base.BeginProperties( showColor: false );

			EditorGUILayout.PropertyField( propColorMode );
			if( propColorMode.enumValueIndex == (int)Triangle.TriangleColorMode.Single ) {
				ShapesUI.PosColorField( "A", propA, base.propColor );
				ShapesUI.PosColorField( "B", propB, base.propColor, false );
				ShapesUI.PosColorField( "C", propC, base.propColor, false );
			} else {
				ShapesUI.PosColorField( "A", propA, base.propColor );
				ShapesUI.PosColorField( "B", propB, propColorB );
				ShapesUI.PosColorField( "C", propC, propColorC );
			}

			base.EndProperties();
		}

	}

}