import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router";
import { GlobalContext } from "../context/GlobalContext";
import CarItem from "../components/CarItem.jsx";
import BidItem from "../components/BidItem.jsx";

function CarDetail() {
  const { carItem, auction } = useContext(GlobalContext);
  const { id } = useParams();
  console.log(id);
  console.log(carItem);
  const carId = carItem.find((car) => car.id === id);
  console.log("CarId", carId);
  return (
    <>
      <div className="container-xl">
        <div className="row">
          {<CarItem item={carId} key={id} />}
          <div className="col col-lg-3">
            <BidItem item={carId} />
          </div>
        </div>
      </div>
    </>
  );
}

export default CarDetail;
