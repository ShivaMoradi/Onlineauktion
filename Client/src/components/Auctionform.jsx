import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext.jsx";

function formatDateTime(date) {
  const d = new Date(date);
  const year = d.getFullYear();
  const month = `${d.getMonth() + 1}`.padStart(2, "0");
  const day = `${d.getDate()}`.padStart(2, "0");
  const hour = `${d.getHours()}`.padStart(2, "0");
  const minute = `${d.getMinutes()}`.padStart(2, "0");
  const second = `${d.getSeconds()}`.padStart(2, "0");
  return `${year}-${month}-${day} ${hour}:${minute}:${second}`;
}

function formatEndTime(date) {
  const d = new Date(date);
  const year = d.getFullYear();
  const month = `${d.getMonth() + 1}`.padStart(2, "0");
  const day = `${d.getDate()}`.padStart(2, "0");
  return `${year}-${month}-${day}`;
}

function AuctionForm({ onSubmit, closeForm, auction }) {
  const { user } = useContext(AuthContext);

  const [auctionForm, setAuctionForm] = useState({
    userId: user?.id || undefined,
    brand: auction?.brand || "",
    model: auction?.model || "",
    year: auction?.year || "",
    color: auction?.color || "",
    mileage: auction?.mileage || "",
    engineType: auction?.engineType || "",
    engineDisplacement: auction?.engineDisplacement || "",
    transmission: auction?.transmission || "",
    features: auction?.features ? auction.features.join(", ") : "",
    price: auction?.price || "",
    imageUrl: auction?.imageUrl || "",
    title: auction?.title || "",
    startDate: auction?.startTime || formatDateTime(new Date()),
    endDate: auction?.endTime ? formatEndTime(auction.endTime) : "",
    highestBid: auction?.highestBid || "",
    status: "0",
  });

  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setAuctionForm((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleFeatureChange = (e) => {
    const features = e.target.value;
    setAuctionForm((prevState) => ({ ...prevState, features }));
  };

  const validateForm = () => {
    const errors = {};

    if (!auctionForm.userId) {
      errors.userId = "No user found.";
    }

    if (!auctionForm.brand.trim()) {
      errors.brand = "Brand is required.";
    }

    if (!auctionForm.model.trim()) {
      errors.model = "Model is required.";
    }

    if (
      !auctionForm.year ||
      auctionForm.year < 1900 ||
      auctionForm.year > new Date().getFullYear() + 1
    ) {
      errors.year = "Invalid year.";
    }

    const endDateTime = new Date(auctionForm.endDate);
    const lastEditedDate = new Date();

    if (
      !auctionForm.endDate ||
      endDateTime <= new Date(auctionForm.startDate) ||
      endDateTime <= lastEditedDate
    ) {
      errors.endDate = "End date must be in the future.";
    }

    if (!auctionForm.color.trim()) {
      errors.color = "Color is required.";
    }

    if (!auctionForm.imageUrl.trim()) {
      errors.imageUrl = "Image URL is required.";
    }

    if (
      !auctionForm.mileage ||
      isNaN(auctionForm.mileage) ||
      auctionForm.mileage < 0
    ) {
      errors.mileage = "Please enter valid mileage.";
    }

    if (!auctionForm.engineType.trim()) {
      errors.engineType = "Please enter engine type.";
    }

    if (!auctionForm.engineDisplacement.trim()) {
      errors.engineDisplacement = "Please enter engine displacement.";
    }

    if (!auctionForm.transmission || auctionForm.transmission === "") {
      errors.transmission = "Transmission type is required.";
    }

    if (!auctionForm.highestBid || auctionForm.highestBid < 0) {
      errors.highestBid = "Please enter a valid starting bid.";
    }

    setErrors(errors);
    return errors;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const errors = validateForm();

    if (Object.keys(errors).length > 0) {
      setErrors(errors);
      return;
    }

    const lastEditedDate = formatDateTime(new Date());
    const imageAlt = `${auctionForm.year} ${auctionForm.brand} ${auctionForm.model}`;

    const carData = {
      brand: auctionForm.brand,
      model: auctionForm.model,
      price: auctionForm.highestBid,
      year: auctionForm.year,
      color: auctionForm.color,
      imageUrl: auctionForm.imageUrl,
      mileage: auctionForm.mileage,
      engine_type: auctionForm.engineType,
      engine_displacement: auctionForm.engineDisplacement,
      transmission: auctionForm.transmission,
      features: auctionForm.features,
    };

    const method = auction?.id ? "PUT" : "POST";
    const carEndpoint = auction?.id
      ? `/api/cars/${auction.carId}`
      : "/api/cars/getid";
    const auctionEndpoint = auction?.id
      ? `/api/auctions/${auction.id}`
      : "/api/auctions";

    try {
      const carResponse = await fetch(carEndpoint, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(carData),
      });

      const carId = carResponse.headers.get("Location");

      if (!carResponse.ok)
        throw new Error(`Problem posting car data (with method: ${method}).`);

      const endDateWithTime = new Date(auctionForm.endDate);
      endDateWithTime.setHours(new Date().getHours());
      endDateWithTime.setMinutes(new Date().getMinutes());
      endDateWithTime.setSeconds(new Date().getSeconds());

      const formattedEndTime = formatDateTime(endDateWithTime);
      const auctionData = {
        title: auctionForm.title,
        startTime: auctionForm.startDate,
        endTime: formattedEndTime,
        highestBid: auctionForm.highestBid,
        carId: carId,
        userId: auctionForm.userId,
        status: auctionForm.status,
      };

      const auctionResponse = await fetch(auctionEndpoint, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(auctionData),
      });

      if (!auctionResponse.ok)
        throw new Error(
          `Problem posting auction data (with method: ${method}).`
        );

      if (carResponse.ok && auctionResponse.ok) {
        alert("The car and auction data is now saved in the database!");
      }
    } catch (error) {
      console.error(error);
      alert(error.message);
    }
  };

  return (
    <div className="auction-form-container" data-test="auction-form-container">
      <form
        onSubmit={handleSubmit}
        className="container bg-light p-4 my-5 rounded"
        style={{ backdropFilter: "blur(10px)" }}
        data-test="auction-form"
      >
        <div className="row g-2">
          <div className="col-12">
            <div className="card mb-4" data-test="auction-details-card">
              <div className="col-12">
                <div className="card">
                  <div className="card-header">Auction Details</div>
                  <div className="card-body">
                    <div className="row g-3">
                      <div className="col-md-6">
                        <label htmlFor="title" className="form-label">
                          Title:
                        </label>
                        <input
                          type="text"
                          className="form-control"
                          id="title"
                          name="title"
                          value={auctionForm.title}
                          onChange={handleChange}
                          data-test="title-input"
                        />
                        {errors.title && <div className="text-danger">{errors.title}</div>}
                      </div>
                      <div className="col-md-6">
                        <label htmlFor="endDate" className="form-label">
                          End Date:
                        </label>
                        <input
                          type="date"
                          className="form-control"
                          id="endDate"
                          name="endDate"
                          value={auctionForm.endDate}
                          onChange={handleChange}
                          data-test="end-date-input"
                        />
                        {errors.endDate && <div className="text-danger">{errors.endDate}</div>}
                      </div>
                      <div className="col-md-6">
                        <label htmlFor="highestBid" className="form-label">
                          Starting Bid:
                        </label>
                        <input
                          type="number"
                          className="form-control"
                          id="highestBid"
                          name="highestBid"
                          value={auctionForm.highestBid}
                          onChange={handleChange}
                          data-test="starting-bid-input"
                        />
                        {errors.highestBid && <div className="text-danger">{errors.highestBid}</div>}
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div className="card-header">Car Details</div>
              <div className="card-body" data-test="car-details-card">
                <div className="row g-3">
                  <div className="col-md-6">
                    <label htmlFor="brand" className="form-label">
                      Brand:
                    </label>
                    <input
                      type="text"
                      className="form-control"
                      id="brand"
                      name="brand"
                      value={auctionForm.brand}
                      onChange={handleChange}
                      data-test="brand-input"
                    />
                    {errors.brand && <div className="text-danger">{errors.brand}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="model" className="form-label">
                      Model:
                    </label>
                    <input
                      type="text"
                      className="form-control"
                      id="model"
                      name="model"
                      value={auctionForm.model}
                      onChange={handleChange}
                      data-test="model-input"
                    />
                    {errors.model && <div className="text-danger">{errors.model}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="year" className="form-label">
                      Year:
                    </label>
                    <input
                      type="number"
                      className="form-control"
                      id="year"
                      name="year"
                      value={auctionForm.year}
                      onChange={handleChange}
                      data-test="year-input"
                    />
                    {errors.year && <div className="text-danger">{errors.year}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="color" className="form-label">
                      Color:
                    </label>
                    <input
                      type="text"
                      className="form-control"
                      id="color"
                      name="color"
                      value={auctionForm.color}
                      onChange={handleChange}
                      data-test="color-input"
                    />
                    {errors.color && <div className="text-danger">{errors.color}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="imageUrl" className="form-label">
                      Image URL:
                    </label>
                    <input
                      type="text"
                      className="form-control"
                      id="imageUrl"
                      name="imageUrl"
                      placeholder="Enter image URL"
                      value={auctionForm.imageUrl}
                      onChange={handleChange}
                      data-test="image-url-input"
                    />
                    {errors.imageUrl && <div className="text-danger">{errors.imageUrl}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="mileage" className="form-label">
                      Mileage:
                    </label>
                    <input
                      type="number"
                      className="form-control"
                      id="mileage"
                      name="mileage"
                      value={auctionForm.mileage}
                      onChange={handleChange}
                      data-test="mileage-input"
                    />
                    {errors.mileage && <div className="text-danger">{errors.mileage}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="engineType" className="form-label">
                      Engine Type:
                    </label>
                    <input
                      type="text"
                      className="form-control"
                      id="engineType"
                      name="engineType"
                      value={auctionForm.engineType}
                      onChange={handleChange}
                      data-test="engine-type-input"
                    />
                    {errors.engineType && <div className="text-danger">{errors.engineType}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="engineDisplacement" className="form-label">
                      Engine Displacement:
                    </label>
                    <input
                      type="text"
                      className="form-control"
                      id="engineDisplacement"
                      name="engineDisplacement"
                      value={auctionForm.engineDisplacement}
                      onChange={handleChange}
                      data-test="engine-displacement-input"
                    />
                    {errors.engineDisplacement && <div className="text-danger">{errors.engineDisplacement}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="transmission" className="form-label">
                      Transmission:
                    </label>
                    <select
                      className="form-select"
                      id="transmission"
                      name="transmission"
                      value={auctionForm.transmission}
                      onChange={handleChange}
                      data-test="transmission-select"
                    >
                      <option value="">Select Transmission</option>
                      <option value="automatic">Automatic</option>
                      <option value="manual">Manual</option>
                    </select>
                    {errors.transmission && <div className="text-danger">{errors.transmission}</div>}
                  </div>
                  <div className="col-md-6">
                    <label htmlFor="features" className="form-label">
                      Features:
                    </label>
                    <input
                      type="text"
                      className="form-control"
                      id="features"
                      name="features"
                      placeholder="Enter features separated by commas"
                      value={auctionForm.features}
                      onChange={handleFeatureChange}
                      data-test="features-input"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="d-grid gap-2 d-md-flex justify-content-center">
          <button
            type="submit"
            className="btn btn-primary btn-lg ms-md-2"
            data-test="save-auction-btn"
          >
            Save Auction
          </button>
          <button
            type="button"
            className="btn btn-secondary btn-lg"
            onClick={closeForm}
            data-test="cancel-btn"
          >
            Cancel
          </button>
        </div>
        {Object.keys(errors).length > 0 && (
          <div className="alert alert-danger" role="alert" data-test="form-errors">
            Please correct the errors in the form.
          </div>
        )}
      </form>
    </div>
  );
}

export default AuctionForm;
