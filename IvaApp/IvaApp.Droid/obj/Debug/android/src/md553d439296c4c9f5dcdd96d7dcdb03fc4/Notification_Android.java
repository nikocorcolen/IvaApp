package md553d439296c4c9f5dcdd96d7dcdb03fc4;


public class Notification_Android
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("IvaApp.Droid.Notification_Android, IvaApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Notification_Android.class, __md_methods);
	}


	public Notification_Android () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Notification_Android.class)
			mono.android.TypeManager.Activate ("IvaApp.Droid.Notification_Android, IvaApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
