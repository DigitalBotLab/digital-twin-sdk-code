from abc import ABC, abstractmethod
from typing import Tuple

class IBehaviour(ABC):
    # Assuming the IBehaviour interface has some methods. Placeholder for now.
    pass

class IPhysicsBehaviour(IBehaviour):

    def telemetry_update(self, key_values: Tuple[str, str]):
        # Telemetry data has changed, apply physics
        self.set_physics(object())

    @abstractmethod
    def set_physics(self, updated: object):
        pass

class IPositionBehaviour(IBehaviour):

    def telemetry_update(self, key_values: Tuple[str, str]):
        # Telemetry data has changed, update the position
        self.set_position(object())

    @abstractmethod
    def set_position(self, updated: object):
        pass

class IAnimationBehaviour(IBehaviour):

    def telemetry_update(self, key_values: Tuple[str, str]):
        # Telemetry data has changed, trigger animation
        self.trigger_animation(object())

    @abstractmethod
    def trigger_animation(self, updated: object):
        pass

class IPropertyBehaviour(IBehaviour):

    def telemetry_update(self, key_values: Tuple[str, str]):
        pass

    @abstractmethod
    def set_property(self, updated: object):
        pass
