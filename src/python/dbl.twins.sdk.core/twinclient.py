import json
import asyncio
from typing import List, Callable

# Models
class RoomPatch:
    def __init__(self, value: int, path: str, op: str):
        self.value = value
        self.path = path
        self.op = op

class RoomModel:
    def __init__(self, model_id: str, patch: List[RoomPatch]):
        self.model_id = model_id
        self.patch = patch

# Event arguments
class ConnectionEventArgs:
    def __init__(self, is_connected: bool, message: str):
        self.is_connected = is_connected
        self.message = message

class TelemetryEventArgs:
    def __init__(self, source: str, telemetry_data: str):
        self.source = source
        self.telemetry_data = telemetry_data

# Main Client
class TwinClient:
    def __init__(self, event_hub_connection_string: str):
        self._connection_string = event_hub_connection_string
        parts = self._connection_string.split(';')
        self._event_hub_name = parts[0]
        
        # Events represented as lists of callable functions
        self.telemetry_update_callbacks: List[Callable[[TelemetryEventArgs], None]] = []
        self.connected_callbacks: List[Callable[[ConnectionEventArgs], None]] = []

    async def connect_hub(self):
        # ... (Your Azure EventHubs Python SDK code here)

        # This is just a placeholder to show how you might handle events
        data_received = "{...}"  # Placeholder for the data received from Azure EventHubs
        self._on_telemetry_update("SomeSubject", data_received)

    def _on_connected(self, is_connected: bool, msg: str):
        event_args = ConnectionEventArgs(is_connected, msg)
        for callback in self.connected_callbacks:
            callback(event_args)

    def _on_telemetry_update(self, subject: str, data: str):
        room_model = json.loads(data, cls=RoomModel)
        print(f"Model ID: {room_model.model_id}")
        
        for patch in room_model.patch:
            print(f"Operation: {patch.op}")
            print(f"Path: {patch.path}")
            print(f"Value: {patch.value}")
            
            event_args = TelemetryEventArgs(subject, str(patch.value))
            for callback in self.telemetry_update_callbacks:
                callback(event_args)

# Usage
client = TwinClient("YourConnectionStringHere")

def telemetry_updated(args: TelemetryEventArgs):
    print(f"Telemetry Updated: {args.telemetry_data}")

client.telemetry_update_callbacks.append(telemetry_updated)
asyncio.run(client.connect_hub())
