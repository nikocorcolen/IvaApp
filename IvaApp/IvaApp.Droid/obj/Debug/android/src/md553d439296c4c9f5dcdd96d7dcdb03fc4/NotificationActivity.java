package md553d439296c4c9f5dcdd96d7dcdb03fc4;


public class NotificationActivity
	extends md5d4dd78677dce656d5db26c85a3743ef3.FormsApplicationActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("IvaApp.Droid.NotificationActivity, IvaApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NotificationActivity.class, __md_methods);
	}


	public NotificationActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NotificationActivity.class)
			mono.android.TypeManager.Activate ("IvaApp.Droid.NotificationActivity, IvaApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
