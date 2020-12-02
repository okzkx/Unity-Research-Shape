using System.Linq;
using UnityEngine;
using UnityEditor;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes {

	[CustomEditor( typeof(Line) )]
	[CanEditMultipleObjects]
	public class LineEditor : ShapeRendererEditor {

		SerializedProperty propGeometry = null;
		SerializedProperty propStart = null;
		SerializedProperty propEnd = null;
		SerializedProperty propColorEnd = null;
		SerializedProperty propColorMode = null;
		SerializedProperty propThickness = null;
		SerializedProperty propThicknessSpace = null;
		SerializedProperty propEndCaps = null;
		SerializedProperty propDashStyle = null;
		SerializedProperty propDashed = null;
		SerializedProperty propMatchDashSpacingToSize = null;

		DashStyleEditor dashEditor;

		public override void OnEnable() {
			base.OnEnable();
			dashEditor = DashStyleEditor.GetLineDashEditor( propDashStyle, propMatchDashSpacingToSize, propGeometry, propDashed );
		}

		public override void OnInspectorGUI() {
			SerializedObject so = serializedObject;
			base.BeginProperties( showColor: false );

			bool updateGeometry = false;

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField( propGeometry, new GUIContent( "Geometry" ) );
			if( EditorGUI.EndChangeCheck() )
				updateGeometry = true;

			// shape (positions & thickness)
			ShapesUI.BeginGroup();
			EditorGUILayout.PropertyField( propStart );
			EditorGUILayout.PropertyField( propEnd );
			ShapesUI.FloatInSpaceField( propThickness, propThicknessSpace );
			ShapesUI.EndGroup();

			// style (color, caps, dashes)
			ShapesUI.BeginGroup();
			EditorGUILayout.PropertyField( propColorMode );
			if( (Line.LineColorMode)propColorMode.enumValueIndex == Line.LineColorMode.Single ) {
				base.PropertyFieldColor();
			} else {
				using( new EditorGUILayout.HorizontalScope() ) {
					EditorGUILayout.PrefixLabel( "Colors" );
					base.PropertyFieldColor( GUIContent.none );
					EditorGUILayout.PropertyField( propColorEnd, GUIContent.none );
				}
			}

			using( new EditorGUILayout.HorizontalScope() ) {
				EditorGUILayout.PrefixLabel( "End Caps" );
				if( ShapesUI.DrawTypeSwitchButtons( propEndCaps, ShapesAssets.LineCapButtonContents, 20 ) )
					updateGeometry = true;
			}

			ShapesUI.EndGroup();

			// Dashes
			ShapesUI.BeginGroup();
			dashEditor.DrawProperties();
			ShapesUI.EndGroup();

			base.EndProperties();

			if( updateGeometry ) {
				foreach( Line line in targets.Cast<Line>() )
					line.UpdateMesh();
			}
		}
	}

}