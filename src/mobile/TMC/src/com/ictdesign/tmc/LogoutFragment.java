/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.graphics.Color;
import android.graphics.ColorFilter;
import android.graphics.LightingColorFilter;
import android.graphics.drawable.Drawable;
import android.media.MediaPlayer;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.Button;

/**
 * Implements the logout fragment. Opens a dialog to confirm exiting of the
 * module activity and redirects back to the login activity.
 */

public class LogoutFragment extends Fragment
{
	MediaPlayer mMediaPlayer = new MediaPlayer();

	private OnClickListener onLogoutClickListener = new OnClickListener() {
		public void onClick(View view)
		{
			playSound(R.raw.ohno);
			Drawable myIcon = getResources().getDrawable(
					android.R.drawable.ic_dialog_alert);
			ColorFilter filter = new LightingColorFilter(Color.RED, Color.RED);
			myIcon.setColorFilter(filter);
			new AlertDialog.Builder(getActivity())
					.setIcon(myIcon)
					.setTitle(Constants.LOGOUT)
					.setMessage(Constants.LOGOUT_CONFIRM)
					.setPositiveButton(Constants.OK,
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id)
								{
									playSound(R.raw.bye);
									getActivity().finish();
									// Add possible logout code here?
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
		}
	};

	/**
	 * Sets the layout and onclick listener.
	 */
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.fragment_logout, container,
				false);
		((Button) rootView.findViewById(R.id.logout_logout_b))
				.setOnClickListener(onLogoutClickListener);
		return rootView;
	}
	
	/**
	 * Plays the sound of the id given.
	 * 
	 * @param soundId
	 */
	
	public void playSound(int soundId)
	{
		if (mMediaPlayer.isPlaying())
			mMediaPlayer.stop();
		mMediaPlayer = MediaPlayer.create(getActivity(), soundId);
		mMediaPlayer.setLooping(false);
		mMediaPlayer.start();
	}
}