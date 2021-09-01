/*
* uewebsocket - unreal engine 4 websocket plugin
*
* Copyright (C) 2017 feiwu <feixuwu@outlook.com>
*
*  This library is free software; you can redistribute it and/or
*  modify it under the terms of the GNU Lesser General Public
*  License as published by the Free Software Foundation:
*  version 2.1 of the License.
*
*  This library is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
*  Lesser General Public License for more details.
*
*  You should have received a copy of the GNU Lesser General Public
*  License along with this library; if not, write to the Free Software
*  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
*  MA  02110-1301  USA
*/

#pragma once

#include "UObject/NoExportTypes.h"
#include "Tickable.h"

#if PLATFORM_UWP
#elif PLATFORM_HTML5
#elif PLATFORM_WINDOWS  
#include "Windows/AllowWindowsPlatformTypes.h"
#include "Windows/PreWindowsApi.h"
#include "libwebsockets.h"
#include "Windows/PostWindowsApi.h" 
#else
#include "libwebsockets.h"
#endif

#include <iostream>

#include "WebSocketContext.generated.h"


class UWebSocketBase;
/**
 * 
 */
UCLASS()
class UWebSocketContext : public UObject, public FTickableGameObject
{
	GENERATED_BODY()
public:

	UWebSocketContext();

	~UWebSocketContext();

	void CreateCtx();

	virtual void BeginDestroy() override;

	virtual void Tick(float DeltaTime) override;
	virtual bool IsTickable() const override;
	virtual TStatId GetStatId() const override;

	UWebSocketBase* Connect(const FString& uri, bool& connectFail);
	UWebSocketBase* Connect(const FString& uri, const TMap<FString, FString>& header, bool& connectFail);
#if PLATFORM_UWP
#elif PLATFORM_HTML5
#else
	static int callback_echo(struct lws *wsi, enum lws_callback_reasons reason, void *user, void *in, size_t len);
#endif
	
private:

#if PLATFORM_UWP
#elif PLATFORM_HTML5
#else
	struct lws_context* mlwsContext;
	std::string mstrCAPath;
#endif
};
