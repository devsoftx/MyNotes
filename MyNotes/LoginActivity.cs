
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyNotes
{
	[Activity (Label = "Ingreso", MainLauncher = true, Icon = "@drawable/ic_launcher")]			
	public class LoginActivity : Activity
	{
		private Button btnIngresar;
		private EditText txtUsuario;
		private EditText txtClave;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Login);

			btnIngresar = FindViewById<Button> (Resource.Id.btnIngresar);
			txtUsuario = FindViewById<EditText> (Resource.Id.txtUsuario);
			txtClave = FindViewById<EditText> (Resource.Id.txtClave);
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			btnIngresar.Click += btnIngresar_Click;
		}

		protected void btnIngresar_Click(object sender, EventArgs e){
			string usuario = txtUsuario.Text;
			string clave = txtClave.Text;
			goToMain ();
		}

		protected void goToMain(){
			Intent intent = new Intent (this, typeof(MainActivity));
			StartActivity (intent);
		}
	}
}