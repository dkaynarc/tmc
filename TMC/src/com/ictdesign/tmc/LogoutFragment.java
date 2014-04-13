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
 * A dummy fragment representing a section of the app, but that simply displays
 * dummy text.
 */
public class LogoutFragment extends Fragment
{
	MediaPlayer mMediaPlayer = new MediaPlayer();

	private OnClickListener onLogoutClickListener = new OnClickListener() {
		public void onClick(View view)
		{
			if (mMediaPlayer.isPlaying())
				mMediaPlayer.stop();
			mMediaPlayer = MediaPlayer.create(getActivity(), R.raw.ohno);
			mMediaPlayer.setLooping(false);
			mMediaPlayer.start();
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
									if (mMediaPlayer.isPlaying())
										mMediaPlayer.stop();
									mMediaPlayer = MediaPlayer.create(
											getActivity(), R.raw.bye);
									mMediaPlayer.setLooping(false);
									mMediaPlayer.start();
									getActivity().finish();
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
}