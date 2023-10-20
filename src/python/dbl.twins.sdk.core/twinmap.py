from abc import ABC, abstractmethod
from typing import Tuple

class ITwinMap(ABC):
    # Placeholder for the ITwinMap interface.
    pass

class IBehaviour(ABC):

    @abstractmethod
    def telemetry_update(self, key_values: Tuple[str, str]):
        pass
