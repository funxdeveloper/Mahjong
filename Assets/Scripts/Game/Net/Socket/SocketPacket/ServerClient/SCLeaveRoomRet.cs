﻿using System;
using System.Collections;
using System.Collections.Generic;


public class SCLeaveRoomRet : SocketPacket
{
	public BOOL mResult = new BOOL();
	public SCLeaveRoomRet(PACKET_TYPE type)
		:
		base(type)
	{
		fillParams();
		zeroParams();
	}
	protected override void fillParams()
	{
		pushParam(mResult);
	}
	public override void execute()
	{
		if (mResult.mValue)
		{
			UnityUtility.logInfo("离开房间成功");

			// 销毁房间中的所有其他玩家和房间
			Room room = (mGameSceneManager.getCurScene() as MahjongScene).getRoom();
			room.leaveAllRoomPlayer();
			// 进入到上一个场景
			CommandGameSceneManagerEnter cmdEnter = mCommandSystem.newCmd<CommandGameSceneManagerEnter>();
			cmdEnter.mSceneType = GAME_SCENE_TYPE.GST_MAIN;
			mCommandSystem.pushCommand(cmdEnter, mGameSceneManager);
		}
		else
		{
			UnityUtility.logInfo("离开房间失败");
		}
	}
}