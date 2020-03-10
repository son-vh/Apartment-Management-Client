package md5614f2ff032fa9b27e4e3e6de051a7b1b;


public class EquipmentViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Manager.Adapter.EquipmentViewHolder, Manager", EquipmentViewHolder.class, __md_methods);
	}


	public EquipmentViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == EquipmentViewHolder.class)
			mono.android.TypeManager.Activate ("Manager.Adapter.EquipmentViewHolder, Manager", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
