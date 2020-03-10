package md591bf92fddba20c03cc30232c04ed37a4;


public class MyOnMenuItemClickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.support.v7.widget.PopupMenu.OnMenuItemClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMenuItemClick:(Landroid/view/MenuItem;)Z:GetOnMenuItemClick_Landroid_view_MenuItem_Handler:Android.Support.V7.Widget.PopupMenu/IOnMenuItemClickListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"";
		mono.android.Runtime.register ("AbcPlaza.Adapter.MyOnMenuItemClickListener, AbcPlaza", MyOnMenuItemClickListener.class, __md_methods);
	}


	public MyOnMenuItemClickListener ()
	{
		super ();
		if (getClass () == MyOnMenuItemClickListener.class)
			mono.android.TypeManager.Activate ("AbcPlaza.Adapter.MyOnMenuItemClickListener, AbcPlaza", "", this, new java.lang.Object[] {  });
	}

	public MyOnMenuItemClickListener (md591bf92fddba20c03cc30232c04ed37a4.EquipmentAdapter p0, int p1, android.content.Context p2)
	{
		super ();
		if (getClass () == MyOnMenuItemClickListener.class)
			mono.android.TypeManager.Activate ("AbcPlaza.Adapter.MyOnMenuItemClickListener, AbcPlaza", "AbcPlaza.Adapter.EquipmentAdapter, AbcPlaza:System.Int32, mscorlib:Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public boolean onMenuItemClick (android.view.MenuItem p0)
	{
		return n_onMenuItemClick (p0);
	}

	private native boolean n_onMenuItemClick (android.view.MenuItem p0);

	private java.util.ArrayList refList;
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
