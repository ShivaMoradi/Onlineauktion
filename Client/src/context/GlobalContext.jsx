import { createContext, useState, useEffect } from "react";

const GlobalContext = createContext();
const API_CAR_URL_HOME = "api/cars/home";
const API_CAR_URL = "/api/cars";
const API_USER_URL = "/api/users";
const API_AUTION_URL = "/api/auctions";

function GlobalProvider({ children }) {
  const [value, setValue] = useState(1);
  const [carItem, setCarItem] = useState([]);
  const [user, setUser] = useState([]);
  const [auction, setAuction] = useState([]);
  const [fetchError, setFetchError] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [filteredCartItems, setFilteredCartItems] = useState(true);
  const [duration, setduration] = useState([]);
  const [originalCarItem, setOriginalCarItem] = useState([]);

  useEffect(() => {
    const fetchItems = async () => {
      try {
        const response = await fetch(API_CAR_URL_HOME);
        const responseDuration = await fetch(API_AUTION_URL);

        if (!response.ok && !responseDuration.ok)
          throw Error("Did not receive expected data");
        const listCarItem = await response.json();
        console.log("Context", listCarItem);
        setCarItem(listCarItem);
        setOriginalCarItem(listCarItem);
        setFetchError(null);
      } catch (err) {
        setFetchError(err.message);
      } finally {
        setIsLoading(false);
      }
    };
    fetchItems();
  }, []);

  useEffect(() => {
    const fetchDurations = async () => {
      try {
        const response = await fetch(API_AUTION_URL);
        if (!response.ok) throw Error("Did not receive expected data");
        const listDurationItem = await response.json();
        setAuction(listDurationItem);
        setFetchError(null);
      } catch (err) {
        setFetchError(err.message);
      } finally {
        setIsLoading(false);
      }
    };

    if (carItem.length > 0) {
      fetchDurations();
    }

    console.log("APICARITEM", carItem);
  }, [carItem]);

  return (
    <GlobalContext.Provider
      value={{
        carItem,
        setCarItem,
        fetchError,
        setFetchError,
        isLoading,
        setIsLoading,
        duration,
        setduration,
        auction,
        originalCarItem,
        filteredCartItems,
        setFilteredCartItems,
      }}
    >
      {children}
    </GlobalContext.Provider>
  );
}

export { GlobalProvider, GlobalContext };
