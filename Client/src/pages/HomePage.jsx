import CarsHome from "../components/CarsHome.jsx";
import AddItem from "../components/AddItem.jsx";
import { useContext, useEffect, useState } from "react";
import LoadProgress from "../components/authentiction/LoadProgress.jsx";

function HomePage() {
  const [isLoning, setIsLoning] = useState(true);
  return (
    <>
      <CarsHome />
    </>
  );
}

export default HomePage;
