/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package ictd.activities;

import adapters.ModuleAdapter;
import android.app.ActionBar;
import android.app.AlertDialog;
import android.app.FragmentTransaction;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.graphics.ColorFilter;
import android.graphics.LightingColorFilter;
import android.graphics.drawable.Drawable;
import android.media.MediaPlayer;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.support.v4.view.ViewPager;
import android.view.Menu;
import android.view.MenuItem;
import model.Constants;

/**
 * Implements the main activity, which is the module activity. It contains many
 * fragments which the user then scrolls through.
 */

public class ModuleActivity extends FragmentActivity implements
		ActionBar.TabListener
{
	ModuleAdapter mModuleAdapter;
	ViewPager mViewPager;
	MediaPlayer mMediaPlayer = new MediaPlayer();

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
		mModuleAdapter = new ModuleAdapter(getSupportFragmentManager(), this);

		// Set up the ViewPager with the sections adapter.
		mViewPager = (ViewPager) findViewById(R.id.pager);
		mViewPager.setAdapter(mModuleAdapter);

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
		for (int i = 0; i < mModuleAdapter.getCount(); i++)
		{
			// Create a tab with text corresponding to the page title defined by
			// the adapter. Also specify this Activity object, which implements
			// the TabListener interface, as the callback (listener) for when
			// this tab is selected.
			actionBar.addTab(actionBar.newTab()
					.setText(mModuleAdapter.getPageTitle(i))
					.setTabListener(this));
		}
	}

	/**
	 * Inflate the menu; this adds items to the action bar.
	 */

	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		super.onCreateOptionsMenu(menu);
		getMenuInflater().inflate(R.menu.module, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item)
	{
		if (item.getItemId() == R.id.module_logout)
		{
			playSound(R.raw.ohno);
			Drawable myIcon = getResources().getDrawable(
					android.R.drawable.ic_dialog_alert);
			ColorFilter filter = new LightingColorFilter(Color.RED, Color.RED);
			myIcon.setColorFilter(filter);

			new AlertDialog.Builder(this)
					.setIcon(myIcon)
					.setTitle(Constants.LOGOUT)
					.setMessage(Constants.LOGOUT_CONFIRM)
					.setPositiveButton(Constants.OK,
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id)
								{
									playSound(R.raw.bye);

									// Add possible logout code here?
									// ////////////////////////////////
									SharedPreferences preferences = getSharedPreferences(
											Constants.APP_PERSISTANCE, 0);
									SharedPreferences.Editor ed = preferences
											.edit();
									ed.remove(Constants.USERNAME_KEY);
									ed.commit();
									// ////////////////////////////////
									finish();
								}
							})
					.setNegativeButton(Constants.CANCEL,
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id)
								{
									dialog.cancel();
								}
							}).show();
			return true;
		}
		return false;
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

	public void playSound(int soundId)
	{
		if (mMediaPlayer.isPlaying())
			mMediaPlayer.stop();
		mMediaPlayer = MediaPlayer.create(this, soundId);
		mMediaPlayer.setLooping(false);
		mMediaPlayer.start();
	}
}
