import React, { useEffect, useState } from "react";
import { Spinner } from "react-bootstrap";

function LoadProgress({ isLoading, setIsLoading }) {
  return (
    <>
      {isLoading && (
        <Spinner animation="border" role="status">
          <span className="visually-hidden">Loading...</span>
        </Spinner>
      )}
    </>
  );
}

export default LoadProgress;
