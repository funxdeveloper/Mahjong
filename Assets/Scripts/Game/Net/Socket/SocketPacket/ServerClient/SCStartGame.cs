﻿using System;
using System.Collections;
using System.Collections.Generic;

public class SCStartGame : SocketPacket
{
	protected BYTES mDice = new BYTES(2);
	public SCStartGame(PACKET_TYPE type)
		:
		base(type)
	{
		fillParams();
		zeroParams();
	}
	protected override void fillParams()
	{
		pushParam(mDice);
	}
	public override void execute()
	{
		GameScene gameScene = mGameSceneManager.getCurScene();
		if(gameScene.getSceneType() != GAME_SCENE_TYPE.GST_MAHJONG)
		{
			return;
		}
		// 跳转到掷骰子流程
		CommandGameSceneChangeProcedure cmd = mCommandSystem.newCmd<CommandGameSceneChangeProcedure>();
		cmd.mProcedure = PROCEDURE_TYPE.PT_MAHJONG_RUNNING_DICE;
		mCommandSystem.pushCommand(cmd, mGameSceneManager.getCurScene());

		// 通知麻将场景开始掷骰子
		CommandMahjongSceneNotifyDice cmdDice = mCommandSystem.newCmd<CommandMahjongSceneNotifyDice>();
		cmdDice.mDice = mDice.mValue;
		mCommandSystem.pushCommand(cmdDice, gameScene);
	}
}