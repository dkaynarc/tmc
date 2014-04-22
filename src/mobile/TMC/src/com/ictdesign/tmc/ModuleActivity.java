/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import android.app.ActionBar;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.view.ViewPager;
import android.view.Menu;

/**
 * Implements the main activity, which is the module activity. It contains many
 * fragments which the user then scrolls through.
 */

public class ModuleActivity extends FragmentActivity implements
		ActionBar.TabListener
{
	SectionsPagerAdapter mSectionsPagerAdapter;
	ViewPager mViewPager;
	static int turnedOn = 0;

	/**
	 * Sets the layout, action bar and pager adapter which is used to scroll
	 * through the fragments.
	 */

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_module);

		final ActionBar actionBar = getActionBar();
		actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_TABS);

		// Creates adapter that will return a fragment for each primary
		// section of the app.
		mSectionsPagerAdapter = new SectionsPagerAdapter(
				getSupportFragmentManager());

		// Set up the ViewPager with the sections adapter.
		mViewPager = (ViewPager) findViewById(R.id.pager);
		mViewPager.setAdapter(mSectionsPagerAdapter);

		// When swiping between different sections, select the corresponding
		// tab. We can also use the action bar's tabs to do this.
		mViewPager
				.setOnPageChangeListener(new ViewPager.SimpleOnPageChangeListener() {
					@Override
					public void onPageSelected(int position)
					{
						actionBar.setSelectedNavigationItem(position);
					}
				});

		// For each of the sections in the app, add a tab to the action bar.
		for (int i = 0; i < mSectionsPagerAdapter.getCount(); i++)
		{
			// Create a tab with text corresponding to the page title defined by
			// the adapter. Also specify this Activity object, which implements
			// the TabListener interface, as the callback (listener) for when
			// this tab is selected.
			actionBar.addTab(actionBar.newTab()
					.setText(mSectionsPagerAdapter.getPageTitle(i))
					.setTabListener(this));
		}
	}

	/**
	 * Inflate the menu; this adds items to the action bar.
	 */

	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		getMenuInflater().inflate(R.menu.module, menu);
		return true;
	}

	/**
	 * When the given tab is selected, switch to the corresponding page in the
	 * ViewPager.
	 */

	@Override
	public void onTabSelected(ActionBar.Tab tab,
			FragmentTransaction fragmentTransaction)
	{
		mViewPager.setCurrentItem(tab.getPosition());
	}

	@Override
	public void onTabUnselected(ActionBar.Tab tab,
			FragmentTransaction fragmentTransaction)
	{
	}

	@Override
	public void onTabReselected(ActionBar.Tab tab,
			FragmentTransaction fragmentTransaction)
	{
	}

	/**
	 * A FragmentPagerAdapter that returns a fragment corresponding to one of
	 * the sections/tabs/pages.
	 */
	public class SectionsPagerAdapter extends FragmentPagerAdapter
	{
		public SectionsPagerAdapter(FragmentManager fm)
		{
			super(fm);
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
			default:
				fragment = new LogoutFragment();
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
			return 4;
		}
		
		/**
		 * Returns the title of each fragment in respect to its position.
		 */

		@Override
		public CharSequence getPageTitle(int position)
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
				return Constants.LOGOUT;
			}
			return null;
		}
	}

}
