/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package adapters;

import ictd.activities.ModuleActivity;
import model.Constants;
import fragments.AlarmsFragment;
import fragments.CompletedOrderFragment;
import fragments.EnvironmentFragment;
import fragments.MachineStatusFragment;
import fragments.OrderQueueFragment;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;

/**
 * A FragmentPagerAdapter that returns a fragment corresponding to one of the
 * sections/tabs/pages.
 */

public class ModuleAdapter extends FragmentPagerAdapter
{
	private Context context;
    private String roleName;



	public ModuleAdapter(FragmentManager fm, Context context)
	{
		super(fm);
		this.context = context;
		roleName = readUserRole();
	}

	/**
	 * getItem is called to instantiate the fragment for the given page.
	 * 
	 * Returns the respective fragment in regards to the position.
	 */

	@Override
	public Fragment getItem(int position)
	{
		Fragment fragment;
		if (roleName.equals(Constants.OPERATOR_ROLE))
		{
			switch (position)
			{
			case 0:
				fragment = new MachineStatusFragment();
				break;
			case 1:
				fragment = new OrderQueueFragment();
				break;
			case 2:
				fragment = new CompletedOrderFragment();
				break;
			case 3:
				fragment = new EnvironmentFragment();
				break;
			default:
				fragment = new AlarmsFragment();
			}
		}
		else
		{
			switch (position)
			{
			case 0:
				fragment = new OrderQueueFragment();
				break;
			default:
				fragment = new CompletedOrderFragment();
				break;
			}
		}
		Bundle args = new Bundle();
		fragment.setArguments(args);
		return fragment;
	}

	/**
	 * Returns the amount of fragments in the module activity.
	 */

	@Override
	public int getCount()
	{
		if (roleName.equals(Constants.OPERATOR_ROLE))
			return 5;
		else
			return 2;
	}

	/**
	 * Returns the title of each fragment in respect to its position.
	 */

	@Override
	public CharSequence getPageTitle(int position)
	{
		if(roleName.equals(Constants.OPERATOR_ROLE))
		{
			switch (position)
			{
			case 0:
				return Constants.MACHINE_STATUS;
			case 1:
				return Constants.ORDER_QUEUE;
			case 2:
				return Constants.COMPLETED_ORDERS;
			case 3:
				return Constants.ENVIRONMENT;
			default:
				return Constants.ALARMS;
			}
		}
		else
			switch (position)
			{
			case 0:
				return Constants.ORDER_QUEUE;
			default:
				return Constants.COMPLETED_ORDERS;
			}
	}
	
	
	
	
	private String readUserRole()
	{
 		SharedPreferences preferences = ((ModuleActivity)context).getSharedPreferences(Constants.APP_PERSISTANCE, 0);
		String userName = preferences.getString(Constants.USERROLE_KEY, null);
		return userName;
	}
}
